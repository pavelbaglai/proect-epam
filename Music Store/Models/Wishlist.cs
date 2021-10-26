using System;
using System.Collections.Generic;

namespace Music_Store.Models
{
    public class Wishlist
    {
        public int ID { get; set; }
        public int CustomerID { get; set; }

        public ICollection<WishlistItem> WishlistItem { get; set; } = new List<WishlistItem>();
        public Customer Customer { get; set; }

        public void AddWishlistItem(int? songID, int? albumID, float price)
        {
            var wishlistItem = new WishlistItem
            {
                SongID = songID,
                AlbumID = albumID,
                CreatedDate = DateTime.Now,
                Price = price,
                Wishlist = this
            };

            WishlistItem.Add(wishlistItem);
        }

        public void RemoveWishlistItem(WishlistItem wishlistItem)
        {
            WishlistItem.Remove(wishlistItem);
        }
    }
}
