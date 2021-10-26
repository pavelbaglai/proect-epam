using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Music_Store.Models.ViewModels
{
    public class CategoryViewModel
    {
        public string Category { get; set; }
        public List<Song> SongList { get; set; }
        public List<Artist> ArtistList { get; set; }
    }
}
