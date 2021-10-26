using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Music_Store.Models.ViewModels
{
    public class CreditCardViewModel
    {

        public int ID { get; set; }

        public int CustomerID { get; set; }

        [Required(ErrorMessage = "Credit card number is required")]
        [DataType(DataType.Text)]
        [Display(Name = "Credit Card")]
        public string CreditCardNumber { get; set; }

        [Required(ErrorMessage = "First name is required!")]
        [DataType(DataType.Text)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required!")]
        [DataType(DataType.Text)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Address is required!")]
        [DataType(DataType.Text)]
        [Display(Name = "Address is required")]
        public string Address { get; set; }

        [Required(ErrorMessage = "City is required!")]
        [DataType(DataType.Text)]
        [Display(Name = "City is required")]
        public string City { get; set; }

        [Required(ErrorMessage = "Province is required!")]
        [DataType(DataType.Text)]
        [Display(Name = "Province is required")]
        public string Provice { get; set; }

        [Required(ErrorMessage = "Postal code is required!")]
        [DataType(DataType.Text)]
        [Display(Name = "Postal code is required")]
        public string PostalCode { get; set; }

        [Required(ErrorMessage = "Country is required!")]
        [DataType(DataType.Text)]
        [Display(Name = "Country is required")]
        public string Country { get; set; }
    }
}
