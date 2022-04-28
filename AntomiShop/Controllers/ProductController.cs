using Antomi.Core.Services.Interfaces;
using Antomi.Data.Entities.Product;
using Antomi.Data.Entities.User;
using Microsoft.AspNetCore.Authorization;
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
            ViewData["BestSellerProducts"] = _productService.GetBestSellerProducts();
            var product = _productService.GetProductForShow(productId);
            return View(product);
        }

        [Route("GetColorName/{colorId}")]
        public IActionResult GetColorName(int colorId)
        {
            return Content(_productService.GetProductColorById(colorId).ColorName);
        }

        [Authorize]
        public IActionResult BuyProduct(int productId,int colorId,int count)
        {
            var user = _userService.GetUserByEmail(User.Identity.Name);
            if (string.IsNullOrEmpty(user.FirstName)|| string.IsNullOrEmpty(user.LastName))
            {
                return Redirect("/UserPanel/UserInformationsConfirm?UserProfileNotConfirmed=true");
            }
            else if (!_userService.IsUSerHasAddress(User.Identity.Name))
            {
                return Redirect("/UserPanel/UserAddresses?UserHasntAddress=true");
            }
            int orderId=_productService.BuyProduct(productId,User.Identity.Name,colorId, count);
            return Redirect("/UserPanel/Orders/OrderDetails/"+orderId);
        }

        [Route("AddComment/{commentText}/{productId}")]
        [Authorize]
        public IActionResult AddComment(string commentText,int productId)
        {
            var comment = new ProductComment();
            comment.UserId = _userService.GetUserIdByEmail(User.Identity.Name);
            comment.CreateDate = DateTime.Now;
            comment.Comment = commentText;
            comment.ProductId = productId;
            _productService.AddProductComment(comment);
            return View("ShowProductComments",_productService.GetProductComments(comment.ProductId));
        }
        [Route("ShowProductComments/{productId}")]
        public IActionResult ShowProductComments(int productId)
        {
            return View(_productService.GetProductComments(productId));
        }
    }
}
