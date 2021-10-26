using System;
using System.Diagnostics.CodeAnalysis;

namespace Music_Store.Models.ViewModels
{
    public class CartItemViewModel : IEquatable<CartItemViewModel>
    {
        public int ID { get; set; }
        public int ItemID { get; set; }
        public int ArtistID { get; set; }
        public string Category { get; set; }
        public string ItemName { get; set; }
        public string ImagePath { get; set; }
        public string ArtistName { get; set; }
        public float Price { get; set; }

        public bool Equals([AllowNull] CartItemViewModel other)
        {
            if (other == null)
            {
                return false;
            }

            return Category.Equals(other.Category, StringComparison.OrdinalIgnoreCase)
                && ItemID == other.ItemID;
        }
    }
}