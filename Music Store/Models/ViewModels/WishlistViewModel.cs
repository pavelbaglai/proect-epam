using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Music_Store.Models.ViewModels
{
    public class WishlistViewModel
    {
        public int ID { get; set; }
        public int CustomerID { get; set; }
        public double TotalPrice { get; set; }

        public List<WishlistItemViewModel> Items { get; set; } = new List<WishlistItemViewModel>();

        public void AddItem(WishlistItemViewModel wishlistItemVm)
        {
            Items.Add(wishlistItemVm);

            var totalPrice = TotalPrice + wishlistItemVm.Price;
            TotalPrice = Math.Round(totalPrice, 2, MidpointRounding.AwayFromZero);
        }
    }
}
