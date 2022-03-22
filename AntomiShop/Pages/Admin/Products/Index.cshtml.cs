using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Antomi.Core.Services.Interfaces;
using Antomi.Core.DTOs.Product;

namespace AntomiShop.Pages.Admin.Products
{
    public class IndexModel : PageModel
    {
        private IProductService _productService;
        public IndexModel(IProductService productService)
        {
            _productService = productService;
        }
        public ShowProductsInAdminViewModel ShowProductsVM { get; set; }
        public void OnGet(int pageId=1,string filterName="")
        {
            ShowProductsVM=_productService.GetProductsForShowInAdmin(pageId,filterName);
        }
        
        public IActionResult OnPostDeleteProduct(int productId)
        {
            //Delete Product
            _productService.DeleteProduct(productId);
            return RedirectToPage("Index");
        }
    }
}
