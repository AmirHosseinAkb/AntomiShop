using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Antomi.Core.Services.Interfaces;
using Antomi.Data.Entities.Product;

namespace AntomiShop.Pages.Admin.Inventory
{
    public class InventoryHistoryModel : PageModel
    {
        private IProductService _productService;
        public InventoryHistoryModel(IProductService productService)
        {
            _productService = productService;
        }
        public List<InventoryHistory> ProductInventories { get; set; }
        public void OnGet(int productId)
        {
            ViewData["ProductTitle"]=_productService.GetProductById(productId).ProductTitle;
            ProductInventories=_productService.GetProductInventoryHistory(productId);
        }
    }
}
