using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Music_Store.Models
{
    public class Publisher
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int EstablishedYear { get; set; }

        public ICollection<Song> Songs { get; set; } = new List<Song>();
        public ICollection<Album> Albums { get; set; } = new List<Album>();
    }
}
