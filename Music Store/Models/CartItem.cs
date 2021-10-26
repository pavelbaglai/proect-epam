using System;

namespace Music_Store.Models
{
    public class CartItem
    {
        public int ID { get; set; }
        public int CartID { get; set; }
        public int? SongID { get; set; }
        public int? AlbumID { get; set; }
        public float Price { get; set; }
        public DateTime CreatedDate { get; set; }
        
        public Song Song { get; set; }
        public Album Album { get; set; }
        public Cart Cart { get; set; }
    }
}