using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Music_Store.Models.ViewModels
{
    public class AlbumViewModel
    {
        public int ID { get; set; }
        
        [Display(Name = "Artist")]
        public string ArtistName { get; set; }
        public int ArtistID { get; set; }

        [Display(Name = "Publisher")]
        public int PublisherID { get; set; }
        public string PublisherName { get; set; }

        public string Name { get; set; }
        public string ImagePath { get; set; }

        [Display(Name = "Image")]
        public IFormFile ImageFile { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Release Date")]
        public DateTime PublishDate { get; set; }

        public double Rating { get; set; }

        [Display(Name = "Favourite")]
        public int FavouriteCount { get; set; }

        [Display(Name = "Purchase")]
        public int PurchaseCount { get; set; }
        public float Price { get; set; }

        [Display(Name = "Genre")]
        public IEnumerable<string> Genres { get; set; }

        public IEnumerable<AlbumSongViewModel> Songs { get; set; }

        public List<ReviewViewModel> Reviews { get; set; }
    }
}