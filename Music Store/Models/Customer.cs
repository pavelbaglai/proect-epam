using System;
using System.Collections.Generic;

namespace Music_Store.Models
{
    public class Customer
    {
        public int ID { get; set; }
        public string Gender { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string CountryCode { get; set; }
        public string PostalCode { get; set; }
        public DateTime DateOfBirth { get; set; }

        public ICollection<Review> Reviews { get; set; } = new List<Review>();
        public ICollection<CreditCard> CreditCards{ get; set; }
        public ICollection<Invoice> Invoices { get; set; }
        public User User { get; set; }
        public Cart Cart { get; set; } = new Cart();
        public Wishlist Wishlist { get; set; } = new Wishlist();

        public void AddReview(int? songID, int? albumID, float rating, string content)
        {
            var review = new Review
            {
                SongID = songID,
                AlbumID = albumID,
                CustomerID = ID,
                Rating = rating,
                Content = content
            };

            Reviews.Add(review);
        }
    }
}
