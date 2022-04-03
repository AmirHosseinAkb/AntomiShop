using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Antomi.Core.Services.Interfaces;
using Antomi.Data.Entities.Order;

namespace AntomiShop.Pages.UserPanel.Orders
{
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
    }
}
