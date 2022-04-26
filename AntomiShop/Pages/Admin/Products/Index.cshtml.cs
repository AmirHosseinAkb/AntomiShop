using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Antomi.Core.Services.Interfaces;
using Antomi.Core.DTOs.Product;
using Antomi.Core.Security;
using Antomi.Data.Entities.Product;
using Microsoft.AspNetCore.Authorization;

namespace AntomiShop.Pages.Admin.Products
{
    [Authorize]
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
        
        public IActionResult OnPostCreateProductColor(int productId,string colorCode,string colorName)
        {
            ProductColor color = new ProductColor()
            {
                ProductId = productId,
                ColorCode = colorCode,
                ColorName = colorName
            };
            _productService.AddColorToProduct(color);
            return RedirectToPage("Index");
        }

        public IActionResult OnPostDeleteColor(int colorId)
        {
            _productService.DeleteColor(colorId);
            return RedirectToPage("Index");
        }

    }
}
