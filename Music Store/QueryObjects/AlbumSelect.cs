using Music_Store.Models;
using Music_Store.Models.ViewModels;
using System;
using System.Linq;

namespace Music_Store.QueryObjects
{
    public static class AlbumSelect
    {
        public static IQueryable<AlbumViewModel> MapAlbumToVM(this IQueryable<Album> albums)
        {
            return albums.Select(a => new AlbumViewModel
            {
                ID = a.ID,
                ArtistName = a.Artist.Stagename,
                ArtistID = a.ArtistID,
                PublisherName = a.Publisher.Name,
                Name = a.Name,
                ImagePath = a.ImagePath,
                PublishDate = a.PublishDate,
                Rating = @Math.Round(
                    a.Reviews.Sum(r => r.Rating) / a.Reviews.Count,
                    1,
                    MidpointRounding.AwayFromZero),
                FavouriteCount = a.FavouriteCount,
                PurchaseCount = a.PurchaseCount,
                Price = a.Price,
                Genres = a.Songs
                        .SelectMany(s => s.SongGenres)
                        .Select(sg => sg.Genre.Name),
                Songs = a.Songs.Select(s => new AlbumSongViewModel
                {
                    ID = s.ID,
                    Name = s.Name,
                    RuntimeInSec = s.RuntimeInSec,
                    Rating = @Math.Round(
                        s.Reviews.Sum(r => r.Rating) / s.Reviews.Count,
                        1,
                        MidpointRounding.AwayFromZero),
                    FavouriteCount = s.FavouriteCount,
                    PurchaseCount = s.PurchaseCount,
                    DataUrl = s.DataUrl,
                }),
                Reviews = a.Reviews.Select(r => new ReviewViewModel
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

        public static IQueryable<CartItemViewModel> MapAlbumToCartItemVM(this IQueryable<Album> albums)
        {
            return albums.Select(a => new CartItemViewModel
            {
                ItemID = a.ID,
                Category = a.GetCategory(),
                ItemName = a.Name,
                ImagePath = a.ImagePath,
                ArtistName = a.Artist.Stagename,
                Price = a.Price
            });
        }

        public static IQueryable<WishlistItemViewModel> MapAlbumToWishlistItemVM(this IQueryable<Album> albums)
        {
            return albums.Select(a => new WishlistItemViewModel
            {
                ItemID = a.ID,
                Category = a.GetCategory(),
                ItemName = a.Name,
                ImagePath = a.ImagePath,
                ArtistName = a.Artist.Stagename,
                Price = a.Price
            });
        }
    }
}
