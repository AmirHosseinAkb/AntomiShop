using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Antomi.Core.Services.Interfaces;
using Antomi.Core.DTOs.Product;
using Microsoft.AspNetCore.Authorization;
using Antomi.Core.Security;

namespace AntomiShop.Pages.Admin.Products
{
    [Authorize]
    [PermissionChecker(new int[] {13,17})]
    public class SpecificProductsModel : PageModel
    {
        private IProductService _productService;
        public SpecificProductsModel(IProductService productService)
        {
            _productService = productService;
        }
        public ShowProductsInAdminViewModel showProductsVM { get; set; }
        public void OnGet(int pageId=1,string filterName="")
        {
            showProductsVM = _productService.GetSpecificProducts(pageId,filterName);
        }

        public IActionResult OnPostRemoveProductFromSpecifics(int productId)
        {
            _productService.RemoveProductFromSpecifics(productId);
            return RedirectToPage("Index");
        }
    }
}
