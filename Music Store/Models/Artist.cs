using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Music_Store.Models
{
    public class Artist
    {
        public int ID { get; set; }
        public string Stagename { get; set; }
        public string Fullname { get; set; }
        public string ImagePath { get; set; }
        public int DebutYear{ get; set; }

        public ICollection<Song> Songs { get; set; } = new List<Song>();
        public ICollection<Album> Albums { get; set; } = new List<Album>();
    }
}