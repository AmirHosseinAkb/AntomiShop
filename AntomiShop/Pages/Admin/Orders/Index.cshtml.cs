using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Antomi.Core.Services.Interfaces;
using Antomi.Data.Entities.Order;

namespace AntomiShop.Pages.Admin.Orders
{
    public class IndexModel : PageModel
    {
        private IOrderService _orderService;
        public IndexModel(IOrderService orderService)
        {
            _orderService = orderService;
        }
        public Tuple<List<Order>, int, int> Orders { get; set; }
        public void OnGet(int pageId=1,string filterName="")
        {
            Orders =_orderService.GetOrdersForShowInAdmin(pageId,filterName);
        }
        public IActionResult OnPostAcceptOrder(int orderId)
        {
            _orderService.AcceptOrder(orderId);
            return Redirect("/Admin/Orders");
        }
        public IActionResult OnGetLoadOrderDetails(int orderId)
        {
            return Partial("_OrderDetailsLoad",_orderService.GetOrderDetails(orderId));
        }
    }
}
