using Microsoft.AspNetCore.Http;
using Music_Store.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;

namespace Music_Store.Models
{
    public class Song : IPurchasable
    {
        public int ID { get; set; }
        public int ArtistID { get; set; }
        public int? AlbumID { get; set; }
        public int PublisherID { get; set; }
        public string Name { get; set; }
        public string ImagePath { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int RuntimeInSec { get; set; }
        public int FavouriteCount { get; set; }
        public int PurchaseCount { get; set; }
        public float Price { get; set; }
        public string DataUrl { get; set; }

        public ICollection<SongGenre> SongGenres { get; set; } = new List<SongGenre>();
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
        public Artist Artist { get; set; }
        public Album Album { get; set; }
        public Publisher Publisher { get; set; }

        public string GetCategory()
        {
            return nameof(Song);
        }
    }
}