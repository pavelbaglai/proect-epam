using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using Music_Store.Data;
using Music_Store.Helpers;
using Music_Store.Models;
using Music_Store.Models.ViewModels;
using Music_Store.Services;

namespace Music_Store.Controllers
{
    public class CartsController : Controller
    {
        private const string SESSION_KEY = "music_store";

        private readonly ApplicationDbContext _context;
        private readonly ICartsService _service;
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;

        public CartsController(ApplicationDbContext context, ICartsService service, SignInManager<User> signInManager, UserManager<User> userManager)
        {
            _context = context;
            _service = service;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        // GET: Carts
        public async Task<IActionResult> Index()
        {
            CartViewModel cartVm = await _service.GetCartFromSessionAsync(HttpContext, SESSION_KEY);

            cartVm = await MergeCartsIfLogedIn(cartVm);

            return View(cartVm);
        }

        // POST: Carts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string type, int id)
        {
            CartViewModel cartVm = await _service.GetCartFromSessionAsync(HttpContext, SESSION_KEY);

            if (!cartVm.Items.Any(i => i.Category == type && i.ItemID == id))
            {
                CartItemViewModel cartItemVm = await _service.CreateCartItemAsync(type, id);
                cartVm.AddItem(cartItemVm);
            }

            cartVm = await MergeCartsIfLogedIn(cartVm);

            return View("Index", cartVm);
        }

        public async Task<IActionResult> CreateWithoutRedirect(string type, int id)
        {
            CartViewModel cartVm = await _service.GetCartFromSessionAsync(HttpContext, SESSION_KEY);

            if (!cartVm.Items.Any(i => i.Category == type && i.ItemID == id))
            {
                CartItemViewModel cartItemVm = await _service.CreateCartItemAsync(type, id);
                cartVm.AddItem(cartItemVm);
            }

            cartVm = await MergeCartsIfLogedIn(cartVm);

            return RedirectToAction("Index", "Wishlist");
        }

        // POST: Carts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int customerID, [FromForm] CartItemViewModel vm, string action)
        {
            if (_signInManager.IsSignedIn(User) && User.IsInRole(nameof(Customer)))
            {
                var itemID = vm.ItemID;
                if (!await _service.CheckIfItemExistsInCartAsync(customerID, itemID))
                {
                    return NotFound();
                }

                await _service.RemoveItem(customerID, itemID);
            }
            else
            {
                var cartVm = await _service.GetCartFromSessionAsync(HttpContext, SESSION_KEY);

                cartVm.Items.Remove(vm);

                HttpContext.Session.SetObjectAsJson(SESSION_KEY, cartVm);
            }

            if (action != null && action == "move")
            {
                return RedirectToAction("CreateWithoutRedirect", "Wishlist", new { type = vm.Category, id = vm.ItemID });
            }

            return RedirectToAction(nameof(Index));
        }

        //private async Task<CartViewModel> GetCartFromSessionAsync(HttpContext httpContext)
        //{
        //    return await Task.Run(() =>
        //    {
        //        var cartVm = httpContext.Session.GetObjectFromJson<CartViewModel>(SESSION_KEY);

        //        if (cartVm == null)
        //        {
        //            cartVm = new CartViewModel();
        //        }

        //        return cartVm;
        //    });
        //}

        private async Task<CartViewModel> MergeCartsIfLogedIn(CartViewModel cartViewModel)
        {
            if (_signInManager.IsSignedIn(User) && User.IsInRole(nameof(Customer)))
            {
                var user = await _userManager.GetUserAsync(User);
                var customerID = user.CustomerID.Value;

                foreach (var i in cartViewModel.Items)
                {
                    if (!await _service.CheckIfItemExistsInCartAsync(customerID, i.ItemID))
                    {
                        await _service.AddItemToCartAsync(i, customerID);
                    }
                }

                cartViewModel = await _service.GetCartFromCustomerAsync(customerID);

                HttpContext.Session.Remove(SESSION_KEY);
            }
            else
            {
                HttpContext.Session.SetObjectAsJson(SESSION_KEY, cartViewModel);
            }

            return cartViewModel;
        }

        
        public ActionResult Checkout()
        {
            return View("Checkout");
        }
    }
}
