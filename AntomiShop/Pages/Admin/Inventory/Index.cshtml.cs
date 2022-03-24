using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Antomi.Core.Services.Interfaces;
using Antomi.Core.DTOs.Product;
using Antomi.Data.Entities.Product;

namespace AntomiShop.Pages.Admin.Inventory
{
    public class IndexModel : PageModel
    {
        private IProductService _productService;
        public IndexModel(IProductService productService)
        {
            _productService = productService;
        }
        public ShowProductsInventoryViewModel ProductsInventory { get; set; }
        public void OnGet(int pageId=1,string filterProductName="")
        {
            ProductsInventory = _productService.GetProductsForShowInventory(pageId, filterProductName);
        }

        public IActionResult OnPostIncreaseInventory(int productId,int increaseCount)
        {

            var inventory = new ProductInventory()
            {
                ProductId = productId,
                ProductCount = increaseCount,
                ChargeDate = DateTime.Now
            };
            _productService.AddInventory(inventory);
            return RedirectToPage("Index");
        }
        public IActionResult OnPostDecreaseInventory(int productId, int decreaseCount)
        {

            var inventory = new ProductInventory()
            {
                ProductId = productId,
                ProductCount = -decreaseCount,
                ChargeDate = DateTime.Now
            };
            _productService.AddInventory(inventory);
            return RedirectToPage("Index");
        }
    }
}
