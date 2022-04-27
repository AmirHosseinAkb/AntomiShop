using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Antomi.Core.Services.Interfaces;
using Antomi.Data.Entities.Product;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using Antomi.Core.Security;

namespace AntomiShop.Pages.Admin.Products
{
    [Authorize]
    [PermissionChecker(13)]
    public class EditProductModel : PageModel
    {
        private IProductService _productService;
        public EditProductModel(IProductService productService)
        {
            _productService = productService;
        }
        public void GetInformations(Product product)
        {
            var Groups = _productService.GetGroups();
            ViewData["Groups"] = new SelectList(Groups, "Value", "Text",product.GroupId);
            var SubGroups = _productService.GetSubGroupsOfGroups(product.GroupId);
            ViewData["SubGroups"] = new SelectList(SubGroups, "Value", "Text",product.SubId);
            var SecSubGroups = _productService.GetSubGroupsOfGroups(product.SubId.Value);
            ViewData["SecSubGroups"] = new SelectList(SecSubGroups, "Value", "Text",product.SecSubId);
        }
        [BindProperty]
        public Product Product { get; set; }
        public void OnGet(int productId)
        {
            Product= _productService.GetProductById(productId);
            GetInformations(Product);
        }
        public IActionResult OnPost(IFormFile? productPic)
        {
            if (!ModelState.IsValid)
            {
                GetInformations(Product);
                return Page();
            }
            //EditProduct
            _productService.EditProduct(Product, productPic);
            return RedirectToPage("Index");
        }
    }
}
