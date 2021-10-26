using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Music_Store.Models;
using Music_Store.Models.ViewModels;
using Music_Store.Services;
using System.Threading.Tasks;

namespace Music_Store.Controllers
{
    [Authorize(Roles = nameof(Customer))]
    public class ReviewsController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IReviewsService _service;

        public ReviewsController(
            UserManager<User> userManager, 
            IReviewsService service)
        {
            _userManager = userManager;
            _service = service;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ReviewViewModel reviewViewModel)
        {
            User user = await _userManager.GetUserAsync(User);
            int customerID = user.CustomerID.Value;

            if (!await CheckCustomerIDWithCurrentUser(reviewViewModel.CustomerID))
            {
                return View("Error", new ErrorViewModel());
            }

            await _service.AddReviewToItemAsync(customerID, reviewViewModel);

            return RedirectToAction(
                "Details", 
                $"{reviewViewModel.Category}s", 
                new { id = reviewViewModel.ItemID });
        }

        // POST: Reviews/5/Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(ReviewViewModel reviewViewModel)
        {
            User user = await _userManager.GetUserAsync(User);
            int customerID = user.CustomerID.Value;

            if (!await CheckCustomerIDWithCurrentUser(reviewViewModel.CustomerID))
            {
                return View("Error", new ErrorViewModel());
            }
            
            await _service.RemoveReviewFromItemAsync(customerID, reviewViewModel.ID);

            return RedirectToAction(
                "Details",
                $"{reviewViewModel.Category}s",
                new { id = reviewViewModel.ItemID });
        }

        private async Task<bool> CheckCustomerIDWithCurrentUser(int customerID)
        {
            User user = await _userManager.GetUserAsync(User);
            int foundCustomerID = user.CustomerID.Value;

            return customerID == foundCustomerID;
        }
    }
}
