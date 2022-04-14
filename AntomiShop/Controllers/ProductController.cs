using Antomi.Core.Services.Interfaces;
using Antomi.Data.Entities.Product;
using Microsoft.AspNetCore.Mvc;

namespace AntomiShop.Controllers
{
    public class ProductController : Controller
    {
        private IProductService _productService;
        private IUserService _userService;
        public ProductController(IProductService productService,IUserService userService)
        {
            _productService = productService;
            _userService = userService;
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
            ViewData["RelatedProducts"]=_productService.GetRelatedProducts(productId);
            return View(_productService.GetProductForShow(productId));
        }
        [Route("GetColorName/{colorId}")]
        public IActionResult GetColorName(int colorId)
        {
            return Content(_productService.GetProductColorById(colorId).ColorName);
        }

        public IActionResult BuyProduct(int productId,int colorId,int count)
        {
            int orderId=_productService.BuyProduct(productId,User.Identity.Name,colorId, count);
            return Redirect("/UserPanel/Orders/OrderDetails/"+orderId);
        }

        public IActionResult AddComment(ProductComment comment)
        {
            comment.UserId = _userService.GetUserIdByEmail(User.Identity.Name);
            comment.CreateDate = DateTime.Now;

        }
    }
}
