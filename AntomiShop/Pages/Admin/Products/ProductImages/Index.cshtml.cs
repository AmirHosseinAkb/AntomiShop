using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Antomi.Core.Services.Interfaces;
using Antomi.Data.Entities.Product;
using Antomi.Core.Security;
using Microsoft.AspNetCore.Authorization;

namespace AntomiShop.Pages.Admin.Products.ProductImages
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private IProductService _productService;
        public IndexModel(IProductService productService)
        {
            _productService = productService;   
        }
        public List<ProductImage> ProductImages { get; set; }
        public void OnGet(int productId)
        {
            ViewData["ProductId"] = productId;
            ProductImages=_productService.GetImagesOfProduct(productId);
        }
        //Admin/Products/ProductImages?handler=ImageChecker
        public IActionResult OnGetImageChecker(IFormFile imgPic)
        {
            return Content(imgPic.IsImage().ToString());
        }

        public IActionResult OnPostAddImage(int productId,IFormFile imgPic)
        {
            _productService.AddImageToProduct(productId, imgPic);
            return RedirectToPage("Index");
        }
        public IActionResult OnPostDeleteImage(int imageId)
        {
            _productService.DeleteImage(imageId);
            return RedirectToPage(imageId);
        }

    }
}
