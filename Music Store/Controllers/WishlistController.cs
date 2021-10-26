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
    public class WishlistController : Controller
    {
        private const string SESSION_KEY_WISHLIST = "wishlist";

        private readonly ApplicationDbContext _context;
        private readonly IWishlistService _service;
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;

        public WishlistController(ApplicationDbContext context, IWishlistService service, SignInManager<User> signInManager, UserManager<User> userManager)
        {
            _context = context;
            _service = service;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        // GET: Wishlist
        public async Task<IActionResult> Index()
        {
            WishlistViewModel wishlistVm = await GetWishlistFromSessionAsync(HttpContext);

            wishlistVm = await MergeWishlistIfLoggedIn(wishlistVm);

            return View(wishlistVm);
        }

        // POST: Wishlist/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string type, int id)
        {
            WishlistViewModel wishlistVm = await GetWishlistFromSessionAsync(HttpContext);

            if (!wishlistVm.Items.Any(item => item.Category == type && item.ItemID == id))
            {
                WishlistItemViewModel wishlistItemVm = await _service.CreateWishlistItemAsync(type, id);
                wishlistVm.AddItem(wishlistItemVm);
            }

            wishlistVm = await MergeWishlistIfLoggedIn(wishlistVm);

            return View("Index", wishlistVm);
        }

        public async Task<IActionResult> CreateWithoutRedirect(string type, int id)
        {
            WishlistViewModel wishlistVm = await GetWishlistFromSessionAsync(HttpContext);

            if (!wishlistVm.Items.Any(item => item.Category == type && item.ItemID == id))
            {
                WishlistItemViewModel wishlistItemVm = await _service.CreateWishlistItemAsync(type, id);
                wishlistVm.AddItem(wishlistItemVm);
            }

            wishlistVm = await MergeWishlistIfLoggedIn(wishlistVm);

            return RedirectToAction("Index", "Carts");
        }

        // POST: Wishlist/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int customerID, [FromForm] WishlistItemViewModel vm, string action)
        {
            if (_signInManager.IsSignedIn(User) && User.IsInRole(nameof(Customer)))
            {
                var itemID = vm.ItemID;
                if (!await _service.CheckIfItemExistsInWishlistAsync(customerID, itemID))
                {
                    return NotFound();
                }

                await _service.RemoveItem(customerID, itemID);
            }
            else
            {
                var wishlistVm = await GetWishlistFromSessionAsync(HttpContext);

                wishlistVm.Items.Remove(vm);

                HttpContext.Session.SetObjectAsJson(SESSION_KEY_WISHLIST, wishlistVm);
            }

            if (action != null && action == "move")
            {
                return RedirectToAction("CreateWithoutRedirect", "Carts", new { type = vm.Category , id = vm.ItemID });
            }

            return RedirectToAction(nameof(Index));
        }

        private async Task<WishlistViewModel> GetWishlistFromSessionAsync(HttpContext httpContext)
        {
            return await Task.Run(() =>
            {
                var wishlistVm = httpContext.Session.GetObjectFromJson<WishlistViewModel>(SESSION_KEY_WISHLIST);

                if (wishlistVm == null)
                {
                    wishlistVm = new WishlistViewModel();
                }

                return wishlistVm;
            });
        }

        private async Task<WishlistViewModel> MergeWishlistIfLoggedIn(WishlistViewModel wishlistViewModel)
        {
            if (_signInManager.IsSignedIn(User) && User.IsInRole(nameof(Customer)))
            {
                var user = await _userManager.GetUserAsync(User);
                var customerID = user.CustomerID.Value;

                foreach (var item in wishlistViewModel.Items)
                {
                    if (!await _service.CheckIfItemExistsInWishlistAsync(customerID, item.ItemID))
                    {
                        await _service.AddItemToWishlistAsync(item, customerID);
                    }
                }

                wishlistViewModel = await _service.GetWishlistFromCustomerAsync(customerID);

                HttpContext.Session.Remove(SESSION_KEY_WISHLIST);
            }
            else
            {
                HttpContext.Session.SetObjectAsJson(SESSION_KEY_WISHLIST, wishlistViewModel);
            }

            return wishlistViewModel;
        }
    }
}
