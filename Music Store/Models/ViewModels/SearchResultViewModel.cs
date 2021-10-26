using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Music_Store.Models.ViewModels
{
    public class SearchResultViewModel
    {
        public string SearchString { get; set; }
        public int SongFoundCount { get; set; }
        public int ArtistFoundCount { get; set; }
        public int AlbumFoundCount { get; set; }
        //public List<Song> SongList { get; set; }
        public IEnumerable<SongViewModel> SongList { get; set; }
        //public List<Artist> ArtistList { get; set; }
        public IEnumerable<AlbumViewModel> AlbumList { get; set; }
        //public List<Album> AlbumList { get; set; }
        public IEnumerable<ArtistViewModel> ArtistList { get; set; }
    }
}
