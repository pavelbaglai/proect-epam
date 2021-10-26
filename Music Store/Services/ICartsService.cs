using Microsoft.AspNetCore.Http;
using Music_Store.Models.ViewModels;
using System.Threading.Tasks;

namespace Music_Store.Services
{
    public interface ICartsService
    {
        Task<CartViewModel> GetCartFromCustomerAsync(int customerID);
        Task<bool> CheckIfItemExistsInCartAsync(int customerID, int itemID);
        Task AddItemToCartAsync(CartItemViewModel cartItemViewModel, int customerID);
        Task<CartItemViewModel> CreateCartItemAsync(string type, int id);
        Task RemoveItem(int customerID, int itemID);
        Task<CartViewModel> GetCartFromSessionAsync(HttpContext httpContext, string sessionKey);
    }
}
