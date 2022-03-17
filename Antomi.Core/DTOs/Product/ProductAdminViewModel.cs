using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Antomi.Core.DTOs.Product
{
    public class ShowProductsInAdminViewModel
    {
        public int ProductId { get; set; }
        public string ProductTitle { get; set; }
        public int ProductPrice { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
