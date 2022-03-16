using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Antomi.Core.Services.Interfaces;
using Antomi.Data.Entities.Product;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AntomiShop.Pages.Admin.Products
{
    public class CreateProductModel : PageModel
    {
        private IProductService _productService;
        public CreateProductModel(IProductService productService)
        {
            _productService = productService;
        }
        [BindProperty]
        public Product Product { get; set; }
        public void OnGet()
        {
            var groups = _productService.GetGroupsForSelect();
            ViewData["Groups"] = new SelectList(groups, "Value", "Text");
            var subGroups=_productService.GetSubGroupsOfGroups(int.Parse(groups.First().Value));
            ViewData["SubGroups"]=new SelectList(subGroups, "Value", "Text"); 
        }

        public IActionResult OnPost()
        {
            return null;
        }
    }
}
