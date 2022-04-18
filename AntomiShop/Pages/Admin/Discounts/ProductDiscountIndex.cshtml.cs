using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Antomi.Core.Services.Interfaces;
using Antomi.Data.Entities.Product;
using System.Globalization;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AntomiShop.Pages.Admin.Discounts
{
    public class ProductDiscountIndexModel : PageModel
    {
        private IProductService _productService;
        public ProductDiscountIndexModel(IProductService productService)
        {
            _productService = productService;
        }
        public Tuple<List<ProductDiscount>,int,int> ProductDiscounts { get; set; }
        public void OnGet(int pageId=1,string filterName="",string stDate="",string edDate="")
        {
            if (!string.IsNullOrEmpty(stDate))
            {
                string[] splitStartDate = stDate.Split('/');
                stDate = new DateTime(int.Parse(splitStartDate[0]),
                    int.Parse(splitStartDate[1]),
                    int.Parse(splitStartDate[2]),
                    new PersianCalendar()).ToString();
            }
            if (!string.IsNullOrEmpty(edDate))
            {
                string[] splitEndDate= edDate.Split('/');
                edDate = new DateTime(int.Parse(splitEndDate[0]),
                    int.Parse(splitEndDate[1]),
                    int.Parse(splitEndDate[2]),
                    new PersianCalendar()).ToString();
            }
            var products = _productService.GetProductsForSelect();
            List<SelectListItem> list = new List<SelectListItem>()
            {
                new SelectListItem(){Text="لطفا محصولی را انتخاب کنید", Value=""}
            };
            list.AddRange(products);
            ViewData["Products"] = new SelectList(list, "Value", "Text");
            ProductDiscounts = _productService.GetProductDiscountsForShow(pageId,filterName,stDate,edDate);
        }

        [BindProperty]
        public ProductDiscount ProductDiscount { get; set; }
        public IActionResult OnPostCreateDiscount(string stDate="",string edDate="")
        {
            if (!string.IsNullOrEmpty(stDate))
            {
                string[] splitStartDate = stDate.Split('/');
                ProductDiscount.StartDate= new DateTime(int.Parse(splitStartDate[0]),
                    int.Parse(splitStartDate[1]),
                    int.Parse(splitStartDate[2]),
                    new PersianCalendar());
            }
            if (!string.IsNullOrEmpty(edDate))
            {
                string[] splitEndDate = edDate.Split('/');
                ProductDiscount.EndDate = new DateTime(int.Parse(splitEndDate[0]),
                    int.Parse(splitEndDate[1]),
                    int.Parse(splitEndDate[2]),
                    new PersianCalendar());
            }
            _productService.AddProductDiscount(ProductDiscount);
            return RedirectToPage("ProductDiscountIndex");
        }
    }
}
