using Music_Store.Models;
using Music_Store.Models.ViewModels;
using System;
using System.Linq;

namespace Music_Store.QueryObjects
{
    public static class SongSelect
    {
        public static IQueryable<SongViewModel> MapSongToVM(this IQueryable<Song> songs)
        {
            return songs.Select(s => new SongViewModel
            {
                ID = s.ID,
                ArtistID = s.ArtistID,
                AlbumID = s.AlbumID,
                PublisherID = s.PublisherID,
                ArtistName = s.Artist.Stagename,
                AlbumName = s.Album.Name,
                PublisherName = s.Publisher.Name,
                Name = s.Name,
                ImagePath = s.ImagePath,
                ReleaseDate = s.ReleaseDate,
                RuntimeInSec = s.RuntimeInSec,
                Rating = @Math.Round(
                    s.Reviews.Sum(r => r.Rating) / s.Reviews.Count,
                    1,
                    MidpointRounding.AwayFromZero),
                FavouriteCount = s.FavouriteCount,
                PurchaseCount = s.PurchaseCount,
                Price = s.Price,
                DataUrl = s.DataUrl,
                Genres = s.SongGenres.Select(sg => sg.Genre.Name).ToList(),
                Reviews = s.Reviews.Select(r => new ReviewViewModel
                {
                    ID = r.ID,
                    Rating = r.Rating,
                    Content = r.Content,
                    WrittenDate = r.WrittenDate,
                    CustomerID = r.CustomerID,
                    CustomerNickName = r.Customer.User.Nickname,
                    CustomerImagePath = r.Customer.User.ImagePath
                }).OrderByDescending(r => r.WrittenDate).ToList()
            });
        }

        public static IQueryable<CartItemViewModel> MapSongToCartItemVM(this IQueryable<Song> songs)
        {
            return songs.Select(s => new CartItemViewModel
            {
                ItemID = s.ID,
                ArtistID = s.ArtistID,
                Category = s.GetCategory(),
                ItemName = s.Name,
                ImagePath = s.ImagePath,
                ArtistName = s.Artist.Stagename,
                Price = s.Price
            });
        }

        public static IQueryable<WishlistItemViewModel> MapSongToWishlistItemVM(this IQueryable<Song> songs)
        {
            return songs.Select(s => new WishlistItemViewModel
            {
                ItemID = s.ID,
                ArtistID = s.ArtistID,
                Category = s.GetCategory(),
                ItemName = s.Name,
                ImagePath = s.ImagePath,
                ArtistName = s.Artist.Stagename,
                Price = s.Price
            });
        }
    }
}
