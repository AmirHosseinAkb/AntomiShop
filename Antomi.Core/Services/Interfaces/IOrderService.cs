using Antomi.Core.DTOs.Discount;
using Antomi.Data.Entities.Discount;
using Antomi.Data.Entities.Order;
using Antomi.Data.Entities.User;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        void DeleteOrder(int orderId);
        void AddUserDiscount(UserDiscount userDiscount);
        List<SelectListItem> GetUserAddressesForSelectInOrder(string email);
        Tuple<List<Order>,int,int> GetOrdersForShowInAdmin(int pageId = 1, string filterName = "");
        void AcceptOrder(int orderId);
        List<OrderDetail> GetOrderDetails(int orderId);
        List<Discount> GetAllDiscountsForShowInAdmin();
        bool IsExistDiscount(string code);
        void AddDiscount(Discount discount);
        void UpdateDiscount(Discount discount);
        void DeleteDiscount(int discountId);
        Order GetUserCard(string email);
        void ChangeOrderDetailCount(int detailId, string type);
    }
}
