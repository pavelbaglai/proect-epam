using System;
using System.Collections.Generic;

namespace Music_Store.Models
{
    public class Cart
    {
        public int ID { get; set; }
        public int CustomerID { get; set; }

        public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
        public Customer Customer { get; set; }

        public void AddCartItem(int? songID, int? albumID, float price)
        {
            var cartItem = new CartItem
            {
                SongID = songID,
                AlbumID = albumID,
                CreatedDate = DateTime.Now,
                Price = price,
                Cart = this
            };

            CartItems.Add(cartItem);
        }

        public void RemoveCartItem(CartItem cartItem)
        {
            CartItems.Remove(cartItem);
        }
    }
}