using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Music_Store.Models.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Email is required!")]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required!")]
        [StringLength(
            100,
            ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.",
            MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare(
            "Password",
            ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "First name is required!")]
        [DataType(DataType.Text)]
        [Display(Name = "First Name")]
        public string Firstname { get; set; }

        [Required(ErrorMessage = "Last name is required!")]
        [DataType(DataType.Text)]
        [Display(Name = "Last Name")]
        public string Lastname { get; set; }

        [Required(ErrorMessage = "Nick name is required!")]
        [DataType(DataType.Text)]
        [Display(Name = "Nick Name")]
        public string Nickname { get; set; }

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
    }
}
