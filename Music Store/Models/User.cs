using Microsoft.AspNetCore.Identity;

namespace Music_Store.Models
{
    public class User : IdentityUser
    {
        public User() : base() { }
        public int? CustomerID { get; set; }
        public int? EmployeeID { get; set; }
        [PersonalData]
        public string Firstname { get; set; }
        [PersonalData]
        public string Lastname { get; set; }
        [PersonalData]
        public string Nickname { get; set; }
        [PersonalData]
        public string ImagePath { get; set; } = $"~/res/images/etc/default_profile.png";

        public Customer Customer { get; set; }
        public Employee Employee { get; set; }
    }
}
