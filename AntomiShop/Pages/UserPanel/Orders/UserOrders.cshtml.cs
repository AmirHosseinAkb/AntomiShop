using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Antomi.Core.Services.Interfaces;
using Antomi.Data.Entities.Order;
using Microsoft.AspNetCore.Authorization;

namespace AntomiShop.Pages.UserPanel.Orders
{
    [Authorize]
    public class UserOrdersModel : PageModel
    {
        private IOrderService _orderService;
        public UserOrdersModel(IOrderService orderService)
        {
            _orderService=orderService;
        }
        public List<Order> Orders { get; set; }
        public void OnGet()
        {
            Orders = _orderService.GetUserOrders(User.Identity.Name);
        }

        public IActionResult OnPostDeleteOrder(int orderId)
        {
            _orderService.DeleteOrder(orderId);
            return RedirectToPage("UserOrders");
        }
    }
}
