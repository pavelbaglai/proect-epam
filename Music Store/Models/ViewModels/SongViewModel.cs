using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Music_Store.Models.ViewModels
{
    public class SongViewModel
    {
        public int ID { get; set; }
        public int ArtistID { get; set; }
        public int? AlbumID { get; set; }
        public int PublisherID { get; set; }

        public string Name { get; set; }
        public string ImagePath { get; set; }

        [Display(Name = "Image")]
        public IFormFile ImageFile { get; set; }

        [Display(Name = "Artist")]
        public string ArtistName { get; set; }

        [Display(Name = "Album")]
        public string AlbumName { get; set; }

        [Display(Name = "Publisher")]
        public string PublisherName { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Release Date")]
        public DateTime ReleaseDate { get; set; }

        [Display(Name = "Runtime")]
        public int RuntimeInSec { get; set; }

        public double Rating { get; set; }

        [Display(Name = "Favourite")]
        public int FavouriteCount { get; set; }

        [Display(Name = "Purchase")]
        public int PurchaseCount { get; set; }
        public float Price { get; set; }
        public string DataUrl { get; set; }

        [Display(Name = "Genre")]
        public List<string> Genres { get; set; }
        public List<ReviewViewModel> Reviews { get; set; }
    }
}
