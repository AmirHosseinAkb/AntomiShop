using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Antomi.Core.Security
{
    public static class ImageChecker
    {
        public static bool IsImage(this IFormFile value)
        {
            try
            {
                var image = System.Drawing.Image.FromStream(value.OpenReadStream());
                return true;
            }
            catch
            {
                return false;            
            }
        }
    }
}
