using Antomi.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AntomiShop.Controllers
{
    public class ProductController : Controller
    {
        private IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        public IActionResult Index(int pageId = 1, string filterProductName = "", string orderType = "createDate"
            ,int minPrice = 0, int maxPrice = 0, List<int> selectedGroups = null, int take = 12)
        {
            ViewData["SelectedGroup"] = selectedGroups;
            return View(_productService.GetProducts(pageId,filterProductName,orderType,minPrice,maxPrice,selectedGroups,take));
        }
        [Route("ShowProduct/{productId}")]
        public IActionResult ShowProduct(int productId)
        {
            return View(_productService.GetProductForShow(productId));
        }
    }
}
