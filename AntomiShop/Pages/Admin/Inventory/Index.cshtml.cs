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
            _productService.ChangeProductInventory(productId,increaseCount);
            InventoryHistory history = new InventoryHistory()
            {
                ProductId = productId,
                Count = increaseCount,
                Date = DateTime.Now
            }; 
            _productService.AddInventoryHistory(history);
            return RedirectToPage("Index");
        }
        public IActionResult OnPostDecreaseInventory(int productId, int decreaseCount)
        {
            _productService.ChangeProductInventory(productId, -decreaseCount);
            InventoryHistory history = new InventoryHistory()
            {
                ProductId = productId,
                Count = -decreaseCount,
                Date = DateTime.Now
            };
            _productService.AddInventoryHistory(history);
            return RedirectToPage("Index");
        }
    }
}
