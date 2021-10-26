using Music_Store.Models.ViewModels;
using System.Threading.Tasks;

namespace Music_Store.Services
{
    public interface IWishlistService
    {
        Task<WishlistViewModel> GetWishlistFromCustomerAsync(int customerID);
        Task<bool> CheckIfItemExistsInWishlistAsync(int customerID, int itemID);
        Task AddItemToWishlistAsync(WishlistItemViewModel wishlistItemViewModel, int customerID);
        Task<WishlistItemViewModel> CreateWishlistItemAsync(string type, int id);
        Task RemoveItem(int customerID, int itemID);
    }
}
