using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Antomi.Core.Services.Interfaces;
using Antomi.Data.Entities.Order;
using Microsoft.AspNetCore.Authorization;

namespace AntomiShop.Pages.UserPanel.Orders
{
    [Authorize]
    public class OrderDetailsModel : PageModel
    {
        private IOrderService _orderService;
        public OrderDetailsModel(IOrderService orderService)
        {
            _orderService = orderService;
        }
        public Order Order { get; set; }
        public void OnGet(int orderId)
        {
            Order=_orderService.GetOrder(User.Identity.Name, orderId);
        }
    }
}
