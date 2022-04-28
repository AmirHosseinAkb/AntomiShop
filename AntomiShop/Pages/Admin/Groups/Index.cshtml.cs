using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Antomi.Core.Services.Interfaces;
using Antomi.Data.Entities.Product;
using Microsoft.AspNetCore.Authorization;
using Antomi.Core.Security;

namespace AntomiShop.Pages.Admin.Groups
{
    [Authorize]
    [PermissionChecker(new int[] {14})]
    public class IndexModel : PageModel
    {
        private IProductService _productService;
        public IndexModel(IProductService productService)
        {
            _productService = productService;
        }
        public List<ProductGroup> ProductGroups { get; set; }
        public void OnGet(string filterName="")
        {
            ProductGroups = _productService.GetAllGroups(filterName);
        }

        public IActionResult OnPostDeleteGroup(int groupId)
        {
            _productService.DeleteGroup(groupId);
            return RedirectToPage("Index");
        }
    }
}
