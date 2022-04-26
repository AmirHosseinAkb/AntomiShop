using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Antomi.Core.Services.Interfaces;
using Antomi.Data.Entities.Discount;
using System.Globalization;
using Microsoft.AspNetCore.Authorization;

namespace AntomiShop.Pages.Admin.Discounts
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private IOrderService _orderService;
        public IndexModel(IOrderService orderService)
        {
            _orderService = orderService;
        }
        public List<Discount> Discounts { get; set; }
        public void OnGet()
        {
            Discounts = _orderService.GetAllDiscountsForShowInAdmin();
        }
        
        public IActionResult OnGetCheckDiscountExistance(string code)
        {
            return Content(_orderService.IsExistDiscount(code).ToString());
        }
        [BindProperty]
        public Discount Discount { get; set; }
        public IActionResult OnPostCreateDiscount(string stDate = "", string edDate = "")
        {
            if (!string.IsNullOrEmpty(stDate))
            {
                string[] splitStartDate = stDate.Split('/');
                Discount.StartDate = new DateTime(int.Parse(splitStartDate[0]),
                    int.Parse(splitStartDate[1]),
                    int.Parse(splitStartDate[2]),
                    new PersianCalendar());
            }
            if (!string.IsNullOrEmpty(edDate))
            {
                string[] splitEndDate = edDate.Split('/');
                Discount.EndDate = new DateTime(int.Parse(splitEndDate[0]),
                    int.Parse(splitEndDate[1]),
                    int.Parse(splitEndDate[2]),
                    new PersianCalendar());
            }
            //Add Discount
            _orderService.AddDiscount(Discount);
            return RedirectToPage("Index");
        }
        public IActionResult OnPostEditDiscount(int discountId,string stDate = "", string edDate = "")
        {
            Discount.DiscountId = discountId;
            if (!string.IsNullOrEmpty(stDate))
            {
                string[] splitStartDate = stDate.Split('/');
                Discount.StartDate = new DateTime(int.Parse(splitStartDate[0]),
                    int.Parse(splitStartDate[1]),
                    int.Parse(splitStartDate[2]),
                    new PersianCalendar());
            }
            if (!string.IsNullOrEmpty(edDate))
            {
                string[] splitEndDate = edDate.Split('/');
                Discount.EndDate = new DateTime(int.Parse(splitEndDate[0]),
                    int.Parse(splitEndDate[1]),
                    int.Parse(splitEndDate[2]),
                    new PersianCalendar());
            }
            //Update Discount
            _orderService.UpdateDiscount(Discount);
            return RedirectToPage("Index");
        }
        public IActionResult OnPostDeleteDiscount(int discountId)
        {
            _orderService.DeleteDiscount(discountId);
            return RedirectToPage("Index");
        }
    }
}
