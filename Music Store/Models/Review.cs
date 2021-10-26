using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Music_Store.Models
{
    public class Review
    {
        public int ID { get; set; }
        public int? SongID { get; set; }
        public int? AlbumID { get; set; }
        public int CustomerID { get; set; }
        public float Rating { get; set; }
        public string Content { get; set; }
        public DateTime WrittenDate { get; set; } = DateTime.Now;

        public Song Song { get; set; }
        public Album Album { get; set; }
        public Customer Customer { get; set; }
    }
}
