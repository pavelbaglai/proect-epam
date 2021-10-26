using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Music_Store.Data;
using Music_Store.Models;
using Music_Store.Models.ViewModels;
using Music_Store.Services;


namespace Music_Store.Controllers
{
    [Authorize(Roles = nameof(Customer))]
    public class CreditCardController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ICartsService _service;
        private readonly UserManager<User> _userManager;

        public CreditCardController(ApplicationDbContext context, ICartsService service, UserManager<User> userManager)

        {
            _context = context;
            _service = service;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            User user = await _userManager.GetUserAsync(User);
            int customerID = user.CustomerID.Value;

            var creditCardViewModels = _context.Customers.Where(c => c.ID == customerID).SelectMany(c => c.CreditCards).Select(cc => new CreditCardViewModel
            {
                ID = cc.ID,
                CustomerID = cc.CustomerID,
                CreditCardNumber = cc.CreditCardNumber,
                FirstName = cc.FirstName,
                LastName = cc.LastName,
                Address = cc.Address,
                City = cc.City,
                Provice = cc.Provice,
                PostalCode = cc.PostalCode,
                Country = cc.Country

            }).AsEnumerable();
            return View(creditCardViewModels);
        }

        public IActionResult Create()
        {
            return View(new CreditCardViewModel());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] CreditCardViewModel creditCardViewModel, string returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                return View(creditCardViewModel);
            }

            User user = await _userManager.GetUserAsync(User);
            int customerID = user.CustomerID.Value;

            var creditCard = new CreditCard();
            creditCard.CreditCardNumber = creditCardViewModel.CreditCardNumber;
            creditCard.FirstName = creditCardViewModel.FirstName;
            creditCard.LastName = creditCardViewModel.LastName;
            creditCard.Address = creditCardViewModel.Address;
            creditCard.City = creditCardViewModel.City;
            creditCard.Provice = creditCardViewModel.Provice;
            creditCard.PostalCode = creditCardViewModel.PostalCode;
            creditCard.Country = creditCardViewModel.Country;
            creditCard.CustomerID = customerID;


            // TODO: put info from creditCardViewModel to CreditCard model
            _context.Add(creditCard);
            _context.SaveChanges();            
            
            var page = (string)TempData["previousPage"];
            if (page.Equals("checkoutFromCart", StringComparison.OrdinalIgnoreCase))
            {
                returnUrl = returnUrl ?? Url.Content("~/Checkout/Create");
                TempData["previousPage"] = "cart";
                return LocalRedirect(returnUrl);
            }
            else if (page.Equals("creditIndexPage", StringComparison.OrdinalIgnoreCase))
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View("Error", new ErrorViewModel());
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int customerID, [FromForm] CreditCardViewModel vm)
        {
            var creditCard = _context.Customers.Where(c => c.ID == customerID).SelectMany(c => c.CreditCards).Where(cc => cc.ID == vm.ID).SingleOrDefault();
            _context.Remove(creditCard);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
