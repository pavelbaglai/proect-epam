using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Music_Store.Data;
using Music_Store.Models;
using Music_Store.Models.Interfaces;
using Music_Store.Models.ViewModels;
using Music_Store.QueryObjects;
using System.Linq;
using System.Threading.Tasks;

namespace Music_Store.Services
{
    public class WishlistService : IWishlistService
    {
        private readonly ApplicationDbContext _context;
        private readonly SignInManager<User> _signInManager;

        public WishlistService(ApplicationDbContext context, SignInManager<User> signInManager)
        {
            _context = context;
            _signInManager = signInManager;
        }

        public async Task<WishlistViewModel> GetWishlistFromCustomerAsync(int customerID)
        {
            return await _context.Customers
                            .Where(c => c.ID == customerID)
                            .Select(c => c.Wishlist)
                            .MapWishlistToVM()
                            .FirstOrDefaultAsync();
        }

        public async Task<bool> CheckIfItemExistsInWishlistAsync(int customerID, int itemID)
        {
            return await _context.Customers
                            .AsNoTracking()
                            .Where(c => c.ID == customerID)
                            .SelectMany(c => c.Wishlist.WishlistItem)
                            .Where(ci => ci.SongID.HasValue ?
                                ci.SongID.Value == itemID : ci.AlbumID.Value == itemID)
                            .AnyAsync();
        }

        public async Task AddItemToWishlistAsync(WishlistItemViewModel wishlistItemVm, int customerID)
        {
            IPurchasable item = null;
            int? songID = null;
            int? albumID = null;
            switch (wishlistItemVm.Category)
            {
                case nameof(Song):
                    item = await _context.Songs
                                    .AsNoTracking()
                                    .Where(s => s.ID == wishlistItemVm.ItemID)
                                    .FirstOrDefaultAsync();
                    songID = item.ID;
                    break;
                case nameof(Album):
                    item = await _context.Albums
                                    .AsNoTracking()
                                    .Where(a => a.ID == wishlistItemVm.ItemID)
                                    .FirstOrDefaultAsync();
                    albumID = item.ID;
                    break;
                default:
                    break;
            }

            var wishlist = await _context.Customers
                                .Include(c => c.Wishlist)
                                    .ThenInclude(c => c.WishlistItem)
                                .Where(c => c.ID == customerID)
                                .Select(c => c.Wishlist)
                                .FirstOrDefaultAsync();

            wishlist.AddWishlistItem(songID, albumID, item.Price);
            await _context.SaveChangesAsync();
        }

        public async Task<WishlistItemViewModel> CreateWishlistItemAsync(string type, int id)
        {
            WishlistItemViewModel wishlistItemVm = null;
            switch (type)
            {
                case nameof(Song):
                    wishlistItemVm = await _context.Songs
                                            .AsNoTracking()
                                            .Where(s => s.ID == id)
                                            .MapSongToWishlistItemVM()
                                            .FirstOrDefaultAsync();
                    break;
                case nameof(Album):
                    wishlistItemVm = await _context.Albums
                                            .AsNoTracking()
                                            .Where(a => a.ID == id)
                                            .MapAlbumToWishlistItemVM()
                                            .FirstOrDefaultAsync();
                    break;
                default:
                    break;
            }

            return wishlistItemVm;
        }

        public async Task RemoveItem(int customerID, int itemID)
        {
            var wishlist = await _context.Customers
                                .Include(c => c.Wishlist)
                                    .ThenInclude(c => c.WishlistItem)
                                .Where(c => c.ID == customerID)
                                .Select(c => c.Wishlist)
                                .FirstOrDefaultAsync();

            var wishlistItem = wishlist.WishlistItem
                            .FirstOrDefault(ci => ci.SongID.HasValue ?
                                ci.SongID.Value == itemID : ci.AlbumID.Value == itemID);

            wishlist.RemoveWishlistItem(wishlistItem);
            await _context.SaveChangesAsync();
        }
    }
}
