using System.ComponentModel.DataAnnotations;

namespace Music_Store.Models.ViewModels
{
    public class AlbumSongViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        
        [Display(Name = "Runtime")]
        public int RuntimeInSec { get; set; }

        public double Rating { get; set; }

        [Display(Name = "Favourite")]
        public int FavouriteCount { get; set; }

        [Display(Name = "Purchase")]
        public int PurchaseCount { get; set; }
        
        public string DataUrl { get; set; }
    }
}
