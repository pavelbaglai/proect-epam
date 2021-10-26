using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Music_Store.Data;
using Music_Store.Helpers;
using Music_Store.Models;
using Music_Store.Models.ViewModels;

namespace Music_Store.Areas.Identity.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
        private const string BASE_FILE_PATH = "~/res/newImages/users/";

        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly string baseSavePath;

        public IndexModel(
            ApplicationDbContext context,
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;

            string rootPath = webHostEnvironment.WebRootPath;
            baseSavePath = Path.Combine(rootPath, "res", "newImages", "users");
        }

        public string Username { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public ProfileViewModel ViewModel { get; set; }

        private async Task LoadAsync(User user)
        {
            var customer = await _context.Customers.FirstOrDefaultAsync(c => c.ID == user.CustomerID);

            Username = user.UserName;

            ViewModel = new ProfileViewModel
            {
                Firstname = user.Firstname,
                Lastname = user.Lastname,
                Gender = customer.Gender,
                Street = customer.Street,
                City = customer.City,
                Province = customer.Province,
                CountryCode = customer.CountryCode,
                PostalCode = customer.PostalCode,
                DateOfBirth = customer.DateOfBirth,
                ImagePath = user.ImagePath,
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            var customer = await _context.Customers.FirstOrDefaultAsync(c => c.ID == user.CustomerID);

            if (ViewModel.Firstname != user.Firstname)
            {
                user.Firstname = ViewModel.Firstname;
            }

            if (ViewModel.Lastname != user.Lastname)
            {
                user.Lastname = ViewModel.Lastname;
            }

            if (ViewModel.City != customer.City)
            {
                customer.City = ViewModel.City;
            }

            if (ViewModel.Province != customer.Province)
            {
                customer.Province = ViewModel.Province;
            }

            if (ViewModel.CountryCode != customer.CountryCode)
            {
                customer.CountryCode = ViewModel.CountryCode;
            }

            if (ViewModel.PostalCode != customer.PostalCode)
            {
                customer.PostalCode = ViewModel.PostalCode;
            }

            if (ViewModel.Gender != customer.Gender)
            {
                customer.Gender = ViewModel.Gender;
            }

            if (ViewModel.DateOfBirth != customer.DateOfBirth)
            {
                customer.DateOfBirth = ViewModel.DateOfBirth;
            }

            if(ViewModel.ImageFile != null)
            {
                string extension = Path.GetExtension(ViewModel.ImageFile.FileName);
                string fileName = user.Email + extension;
                string path = Path.Combine(baseSavePath, fileName);

                await FileHelper.CopyFileToPath(ViewModel.ImageFile, path);

                user.ImagePath = BASE_FILE_PATH + fileName;
            }

            _context.Update(customer);
            await _context.SaveChangesAsync();

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
    }
}
