using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Antomi.Core.Services.Interfaces;
using Antomi.Data.Entities.Product;
using Microsoft.AspNetCore.Authorization;

namespace AntomiShop.Pages.Admin.Groups
{
    [Authorize]
    public class EditGroupModel : PageModel
    {
        private IProductService _productService;
        public EditGroupModel(IProductService productService)
        {
            _productService = productService;
        }
        [BindProperty]
        public ProductGroup ProductGroup { get; set; }
        public void OnGet(int groupId)
        {
            ProductGroup=_productService.GetGroupById(groupId);
        }
        public IActionResult OnPost(IFormFile? groupPic)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            //Edit Group
            _productService.EditGroup(ProductGroup, groupPic);
            return RedirectToPage("Index");
        }
    }
}
