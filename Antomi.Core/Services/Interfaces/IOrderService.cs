using Antomi.Core.DTOs.Discount;
using Antomi.Data.Entities.Order;
using Antomi.Data.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Antomi.Core.Services.Interfaces
{
    public interface IOrderService
    {
        List<Order> GetUserOrders(string email); //For Show User Orders In UserPanel
        Order GetOrder(string email,int orderId);//For Show OrderDetails In OrderDetails Page
        Order GetUserOrder(string email,int orderId);
        DiscountUseType UseDiscount(int orderId, string code);
        Order GetOrderById(int orderId);
        void UpdateOrder(Order order);
        void AddUserDiscount(UserDiscount userDiscount);
    }
}
