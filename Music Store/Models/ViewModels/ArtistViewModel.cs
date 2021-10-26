using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Music_Store.Models.ViewModels
{
    public class ArtistViewModel
    {
        public int ID { get; set; }

        [Display(Name = "Full Name")]
        public string FullName { get; set; }
        public string StageName { get; set; }
        public string ImagePath{ get; set; }

        [Display(Name = "Debut Year")]
        public int DebutYear { get; set; }

        [Display(Name = "Image")]
        public IFormFile ImageFile { get; set; }

        public IEnumerable<string> Genres { get; set; }
        public IEnumerable<ArtistAlbumViewModel> Albums { get; set; }
        public IEnumerable<AlbumSongViewModel> Songs { get; set; }
    }
}
