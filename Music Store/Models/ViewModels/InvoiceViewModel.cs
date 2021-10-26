using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Music_Store.Models.ViewModels
{
    public class InvoiceViewModel
    {
        public int ID { get; set; }
        public int CustomerID { get; set; }
        public double TotalPrice { get; set; }
        public DateTime CreatedDate { get; set; }

        public List<InvoiceDetailViewModel> Items { get; set; } = new List<InvoiceDetailViewModel>();

        public void AddItem(InvoiceDetailViewModel invoiceDetailVm)
        {
            Items.Add(invoiceDetailVm);

            var totalPrice = TotalPrice + invoiceDetailVm.Price;
            TotalPrice = Math.Round(totalPrice, 2, MidpointRounding.AwayFromZero);
        }
    }
}
