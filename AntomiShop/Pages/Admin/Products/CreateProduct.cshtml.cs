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
    public class CreateProductModel : PageModel
    {
        private IProductService _productService;
        public CreateProductModel(IProductService productService)
        {
            _productService = productService;
        }
        public void GetInformations()
        {
            var Groups = _productService.GetGroups();
            ViewData["Groups"] = new SelectList(Groups, "Value", "Text");
            var SubGroups = _productService.GetSubGroupsOfGroups(int.Parse(Groups.First().Value));
            ViewData["SubGroups"] = new SelectList(SubGroups, "Value", "Text");
            var SecSubGroups = _productService.GetSubGroupsOfGroups(int.Parse(SubGroups.First().Value));
            ViewData["SecSubGroups"] = new SelectList(SecSubGroups, "Value", "Text");
        }
        [BindProperty]
        public Product Product { get; set; }
        public void OnGet()
        {
            GetInformations();
        }

        public IActionResult OnPost(IFormFile? productPic)
        {
            if (!ModelState.IsValid)
            {
                GetInformations();
                return Page();
            }
            //Add Product
            _productService.AddProduct(Product, productPic);
            return RedirectToPage("Index");
        }
    }
}
