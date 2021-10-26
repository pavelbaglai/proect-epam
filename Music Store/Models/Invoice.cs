using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Music_Store.Models
{
    public class Invoice
    {
        public int ID { get; set; }
        public int CustomerID { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public ICollection<InvoiceDetail> InvoiceDetails { get; set; } = new List<InvoiceDetail>();
        public Customer Customer { get; set; }

        public void AddInvoiceDetail(int? songID, int? albumID, float price)
        {
            var invoiceDetail = new InvoiceDetail
            {
                SongID = songID,
                AlbumID = albumID,
                Price = price,
                Invoice = this
            };

            InvoiceDetails.Add(invoiceDetail);
        }
    }
}
