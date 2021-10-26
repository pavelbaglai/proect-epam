using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Music_Store.Data;
using Music_Store.Helpers;
using Music_Store.Models;
using Music_Store.Models.Interfaces;
using Music_Store.Models.ViewModels;
using Music_Store.QueryObjects;
using System.Linq;
using System.Threading.Tasks;

namespace Music_Store.Services
{
    public class CartsService : ICartsService
    {
        private readonly ApplicationDbContext _context;
        private readonly SignInManager<User> _signInManager;

        public CartsService(ApplicationDbContext context, SignInManager<User> signInManager)
        {
            _context = context;
            _signInManager = signInManager;
        }

        public async Task<CartViewModel> GetCartFromCustomerAsync(int customerID)
        {
            return await _context.Customers
                            .Where(c => c.ID == customerID)
                            .Select(c => c.Cart)
                            .MapCartToVM()
                            .FirstOrDefaultAsync();
        }

        public async Task<bool> CheckIfItemExistsInCartAsync(int customerID, int itemID)
        {
            return await _context.Customers
                            .AsNoTracking()
                            .Where(c => c.ID == customerID)
                            .SelectMany(c => c.Cart.CartItems)
                            .Where(ci => ci.SongID.HasValue ?
                                ci.SongID.Value == itemID : ci.AlbumID.Value == itemID)
                            .AnyAsync();
        }

        public async Task AddItemToCartAsync(CartItemViewModel cartItemVm, int customerID)
        {
            IPurchasable item = null;
            int? songID = null;
            int? albumID = null;
            switch (cartItemVm.Category)
            {
                case nameof(Song):
                    item = await _context.Songs
                                    .AsNoTracking()
                                    .Where(s => s.ID == cartItemVm.ItemID)
                                    .FirstOrDefaultAsync();
                    songID = item.ID;
                    break;
                case nameof(Album):
                    item = await _context.Albums
                                    .AsNoTracking()
                                    .Where(a => a.ID == cartItemVm.ItemID)
                                    .FirstOrDefaultAsync();
                    albumID = item.ID;
                    break;
                default:
                    break;
            }

            var cart = await _context.Customers
                                .Include(c => c.Cart)
                                    .ThenInclude(c => c.CartItems)
                                .Where(c => c.ID == customerID)
                                .Select(c => c.Cart)
                                .FirstOrDefaultAsync();
            
            cart.AddCartItem(songID, albumID, item.Price);
            await _context.SaveChangesAsync();
        }

        public async Task<CartItemViewModel> CreateCartItemAsync(string type, int id)
        {
            CartItemViewModel cartItemVm = null;
            switch (type)
            {
                case nameof(Song):
                    cartItemVm = await _context.Songs
                                            .AsNoTracking()
                                            .Where(s => s.ID == id)
                                            .MapSongToCartItemVM()
                                            .FirstOrDefaultAsync();
                    break;
                case nameof(Album):
                    cartItemVm = await _context.Albums
                                            .AsNoTracking()
                                            .Where(a => a.ID == id)
                                            .MapAlbumToCartItemVM()
                                            .FirstOrDefaultAsync();
                    break;
                default:
                    break;
            }

            return cartItemVm;
        }

        public async Task RemoveItem(int customerID, int itemID)
        {
            var cart = await _context.Customers
                                .Include(c => c.Cart)
                                    .ThenInclude(c => c.CartItems)
                                .Where(c => c.ID == customerID)
                                .Select(c => c.Cart)
                                .FirstOrDefaultAsync();

            var cartItem = cart.CartItems
                            .FirstOrDefault(ci => ci.SongID.HasValue ?
                                ci.SongID.Value == itemID : ci.AlbumID.Value == itemID);

            cart.RemoveCartItem(cartItem);
            await _context.SaveChangesAsync();
        }

        public async Task<CartViewModel> GetCartFromSessionAsync(HttpContext httpContext, string sessionKey)
        {
            return await Task.Run(() =>
            {
                var cartVm = httpContext.Session.GetObjectFromJson<CartViewModel>(sessionKey);

                if (cartVm == null)
                {
                    cartVm = new CartViewModel();
                }

                return cartVm;
            });
        }
    }
}
