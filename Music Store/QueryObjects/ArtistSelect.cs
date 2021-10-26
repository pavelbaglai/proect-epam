using Music_Store.Models;
using Music_Store.Models.ViewModels;
using System;
using System.Linq;

namespace Music_Store.QueryObjects
{
    public static class ArtistSelect
    {
        public static IQueryable<ArtistViewModel> MapArtistToVM(this IQueryable<Artist> artists)
        {
            return artists.Select(a => new ArtistViewModel
            {
                ID = a.ID,
                FullName = a.Fullname,
                StageName = a.Stagename,
                ImagePath = a.ImagePath,
                DebutYear = a.DebutYear,
                Genres = a.Songs
                        .SelectMany(s => s.SongGenres)
                        .Select(sg => sg.Genre.Name),
                Albums = a.Albums.Select(al => new ArtistAlbumViewModel
                {
                    ID = al.ID,
                    Name = al.Name,
                    ArtistName = a.Stagename,
                    ImagePath = al.ImagePath
                }),
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
                    DataUrl = s.DataUrl
                })
            });
        }
    }
}
