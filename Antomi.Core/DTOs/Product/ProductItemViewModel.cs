using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Antomi.Core.DTOs.Product
{
    public class ProductBoxInformationsViewModel
    {
        public int ProductId { get; set; }
        public string ProductTitle { get; set; }
        public int ProductPrice { get; set; }
        public string ProductImageName { get; set; }
        public int InventoryCount { get; set; }
    }
    public class ShowProductItemsViewModel
    {
        public List<ProductBoxInformationsViewModel> ProductBoxInformations { get; set; }
        public int CurrentPage { get; set; }
        public int PageCount { get; set; }
    }
}
