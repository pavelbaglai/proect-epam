using System;

namespace Music_Store.Models.ViewModels
{
    public class ReviewViewModel
    {
        public int ID { get; set; }
        public float Rating { get; set; }
        public string Content { get; set; }
        public DateTime WrittenDate { get; set; }
        public int ItemID { get; set; }
        public string Category { get; set; }
        public int CustomerID { get; set; }
        public string CustomerNickName { get; set; }
        public string CustomerImagePath { get; set; }
    }
}