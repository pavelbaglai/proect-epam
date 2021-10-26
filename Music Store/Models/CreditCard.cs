using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Music_Store.Models
{
    public class CreditCard
    {
        public int ID { get; set; }
        public int CustomerID { get; set; }
        public string CreditCardNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Provice { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }

        public Customer Customer { get; set; }
    }
}
