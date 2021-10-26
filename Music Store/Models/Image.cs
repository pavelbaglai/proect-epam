using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Music_Store.Models
{
    public class Image
    {
        public IFormFile ImageFile { get; set; }
        public string ImageName { get; set; }
        public string ImagePath { get; set; }
    }
}
