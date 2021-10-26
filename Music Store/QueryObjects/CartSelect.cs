using Music_Store.Models;
using Music_Store.Models.ViewModels;
using System;
using System.Linq;

namespace Music_Store.QueryObjects
{
    public static class CartSelect
    {
        public static IQueryable<CartViewModel> MapCartToVM(this IQueryable<Cart> carts)
        {
            return carts.Select(c => new CartViewModel
            {
                ID = c.ID,
                CustomerID = c.CustomerID,
                TotalPrice = @Math.Round(
                    c.CartItems.Sum(i => i.Price),
                    2,
                    MidpointRounding.AwayFromZero),
                Items = c.CartItems.Select(ci => ci.SongID.HasValue ?
                new CartItemViewModel
                {
                    ID = ci.ID,
                    ItemID = ci.SongID.Value,
                    Category = nameof(Song),
                    ItemName = ci.Song.Name,
                    ImagePath = ci.Song.ImagePath,
                    ArtistName = ci.Song.Artist.Stagename,
                    Price = ci.Song.Price
                }
                :
                new CartItemViewModel
                {
                    ID = ci.ID,
                    ItemID = ci.AlbumID.Value,
                    Category = nameof(Album),
                    ItemName = ci.Album.Name,
                    ImagePath = ci.Album.ImagePath,
                    ArtistName = ci.Album.Artist.Stagename,
                    Price = ci.Album.Price
                }).ToList()
            });
        }
    }
}
