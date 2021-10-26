using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Music_Store.Helpers
{
    public class FileHelper
    {
        public static async Task CopyFileToPath(IFormFile formFile, string path)
        {
            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                await formFile.CopyToAsync(fileStream);
            }
        }
    }
}
