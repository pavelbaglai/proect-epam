using Music_Store.Models.Interfaces;
using System;
using System.Collections.Generic;

namespace Music_Store.Models
{
    public class Album : IPurchasable
    {
        public int ID { get; set; }
        public int PublisherID { get; set; }
        public int ArtistID { get; set; }
        public string Name { get; set; }
        public string ImagePath { get; set; }
        public DateTime PublishDate { get; set; }
        public int FavouriteCount { get; set; }
        public int PurchaseCount { get; set; }
        public float Price { get; set; }

        public ICollection<Song> Songs { get; set; } = new List<Song>();
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
        public Publisher Publisher { get; set; }
        public Artist Artist { get; set; }

        public string GetCategory()
        {
            return nameof(Album);
        }
    }
}
