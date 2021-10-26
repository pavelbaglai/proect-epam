using System;

namespace Music_Store.Models
{
    public class WishlistItem
    {
        public int ID { get; set; }
        public int WishlistID { get; set; }
        public int? SongID { get; set; }
        public int? AlbumID { get; set; }
        public float Price { get; set; }
        public DateTime CreatedDate { get; set; }

        public Song Song { get; set; }
        public Album Album { get; set; }
        public Wishlist Wishlist { get; set; }
    }
}
