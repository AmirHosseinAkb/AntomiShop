using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Antomi.Core.DTOs.Product
{
    public class ShowProductsInAdminViewModel
    {
        public List<Antomi.Data.Entities.Product.Product> Products { get; set; }
        public int CurrentPage { get; set; }
        public int PageCount { get; set; }
    }
    public class InventoryInformationsViewModel
    {
        public int ProductId { get; set; }
        public int ProductPrice { get; set; }
        public string ProductTitle { get; set; }
        public int InventoryCount { get; set; }
        public DateTime CreateDate { get; set; }
    }
    public class ShowProductsInventoryViewModel
    {
        public List<InventoryInformationsViewModel> InventoryInformations { get; set; }
        public int CurrentPage { get; set; }
        public int PageCount { get; set; }
    }
}
