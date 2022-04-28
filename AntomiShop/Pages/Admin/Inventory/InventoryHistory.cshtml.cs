using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Antomi.Core.Services.Interfaces;
using Antomi.Data.Entities.Product;
using Microsoft.AspNetCore.Authorization;
using Antomi.Core.Security;

namespace AntomiShop.Pages.Admin.Inventory
{
    [Authorize]
    [PermissionChecker(new int[] { 2 })]
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
