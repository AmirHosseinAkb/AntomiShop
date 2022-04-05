using Antomi.Core.DTOs.Discount;
using Antomi.Data.Entities.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Antomi.Core.Services.Interfaces
{
    public interface IOrderService
    {
        List<Order> GetUserOrders(string email);
        Order GetOrder(string email,int orderId);
        DiscountUseType UseDiscount(int orderId, string code);
        Order GetOrderById(int orderId);
    }
}
