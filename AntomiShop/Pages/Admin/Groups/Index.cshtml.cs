using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Antomi.Core.Services.Interfaces;
using Antomi.Data.Entities.Product;

namespace AntomiShop.Pages.Admin.Groups
{
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
    }
}
