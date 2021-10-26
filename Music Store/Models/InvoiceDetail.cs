using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Music_Store.Models
{
    public class InvoiceDetail
    {
        public int ID { get; set; }
        public int? SongID { get; set; }
        public int? AlbumID { get; set; }
        public int InvoiceID { get; set; }
        public float Price { get; set; }        

        public Song Song { get; set; }
        public Album Album { get; set; }
        public Invoice Invoice { get; set; }
    }
}
