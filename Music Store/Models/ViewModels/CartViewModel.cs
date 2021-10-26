using System;
using System.Collections.Generic;

namespace Music_Store.Models.ViewModels
{
    public class CartViewModel
    {
        public int ID { get; set; }
        public int CustomerID { get; set; }
        public double TotalPrice { get; set; }

        public List<CartItemViewModel> Items { get; set; } = new List<CartItemViewModel>();

        public void AddItem(CartItemViewModel cartItemVm)
        {
            Items.Add(cartItemVm);

            var totalPrice = TotalPrice + cartItemVm.Price;
            TotalPrice = Math.Round(totalPrice, 2, MidpointRounding.AwayFromZero);
        }
    }
}