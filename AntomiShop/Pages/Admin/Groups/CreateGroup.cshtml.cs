using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Antomi.Core.Services.Interfaces;
using Antomi.Data.Entities.Product;
using Microsoft.AspNetCore.Authorization;

namespace AntomiShop.Pages.Admin.Groups
{
    [Authorize]
    public class CreateGroupModel : PageModel
    {
        private IProductService _productService;
        public CreateGroupModel(IProductService productService)
        {
            _productService = productService;
        }
        [BindProperty]
        public ProductGroup ProductGroup { get; set; }
        public void OnGet(int? parentId=null)
        {
            ProductGroup = new ProductGroup()
            {
                ParentId = parentId
            };
        }
        public IActionResult OnPost(IFormFile? groupPic)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            //Add Group
            _productService.AddGroup(ProductGroup, groupPic);
            return RedirectToPage("Index");
        }
    }
}
