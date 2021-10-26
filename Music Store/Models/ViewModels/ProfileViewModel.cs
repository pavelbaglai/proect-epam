using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Music_Store.Models.ViewModels
{
    public class ProfileViewModel
    {
        [Required(ErrorMessage = "First name is required!")]
        [DataType(DataType.Text)]
        [Display(Name = "First Name")]
        public string Firstname { get; set; }

        [Required(ErrorMessage = "Last name is required!")]
        [DataType(DataType.Text)]
        [Display(Name = "Last Name")]
        public string Lastname { get; set; }

        [Required(ErrorMessage = "Gender is required!")]
        [DataType(DataType.Text)]
        public string Gender { get; set; }

        [Required(ErrorMessage = "Street is required!")]
        [DataType(DataType.Text)]
        public string Street { get; set; }

        [Required(ErrorMessage = "City is required!")]
        [DataType(DataType.Text)]
        public string City { get; set; }

        [Required(ErrorMessage = "Province is required!")]
        [DataType(DataType.Text)]
        public string Province { get; set; }

        [Required(ErrorMessage = "Country Code is required!")]
        [DataType(DataType.Text)]
        [Display(Name = "Country Code")]
        public string CountryCode { get; set; }

        [Required(ErrorMessage = "Postal Code is required!")]
        [DataType(DataType.PostalCode)]
        [Display(Name = "Postal Code")]
        public string PostalCode { get; set; }

        [Required(ErrorMessage = "Date Of Birth is required!")]
        [DataType(DataType.Date)]
        [Display(Name = "Date Of Birth")]
        public DateTime DateOfBirth { get; set; }

        [DataType(DataType.Text)]
        public string ImagePath { get; set; }
        public IFormFile ImageFile { get; set; }
    }
}
