using Antomi.Core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antomi.Data.Context;
using Antomi.Data.Entities.Order;
using Microsoft.EntityFrameworkCore;
using Antomi.Core.DTOs.Discount;
using Antomi.Data.Entities.User;

namespace Antomi.Core.Services
{
    public class OrderService : IOrderService
    {
        private AntomiContext _context;
        private IUserService _userService;
        public OrderService(AntomiContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }

        public void AddUserDiscount(UserDiscount userDiscount)
        {
            _context.UserDiscounts.Add(userDiscount);
            _context.SaveChanges();
        }

        public Order GetOrder(string email, int orderId)
        {
            var userId = _userService.GetUserIdByEmail(email);
            return _context.Orders
                .Include(o => o.OrderDetails)
                .ThenInclude(d => d.Product)
                .Include(o => o.OrderDetails)
                .ThenInclude(d => d.ProductColor)
                .SingleOrDefault(o => o.OrderId == orderId && o.UserId == userId);
        }

        public Order GetOrderById(int orderId)
        {
            return _context.Orders.Find(orderId);
        }

        public List<Order> GetUserOrders(string email)
        {
            int userId = _userService.GetUserIdByEmail(email);
            return _context.Orders.Where(o => o.UserId == userId).ToList();
        }

        public void UpdateOrder(Order order)
        {
            _context.Orders.Update(order);
            _context.SaveChanges();
        }

        public DiscountUseType UseDiscount(int orderId, string code)
        {
            var order = GetOrderById(orderId);
            var discount = _context.Discounts.SingleOrDefault(d => d.DiscountCode == code);
            if (discount == null)
                return DiscountUseType.WrongCode;
            if (discount.StartDate > DateTime.Now)
                return DiscountUseType.ExpireTime;
            if (discount.EndDate < DateTime.Now)
                return DiscountUseType.ExpireTime;
            if (discount.UsableCount < 1)
                return DiscountUseType.Finished;
            if (_context.UserDiscounts.Any(ud => ud.DiscountId == discount.DiscountId && ud.UserId == order.UserId))
                return DiscountUseType.UserUsed;
            var discountPrice = order.OrderSum * discount.DiscountPercent/100;
            order.DiscountPrice = discountPrice;
            order.PaidPrice -= discountPrice;
            UpdateOrder(order);
            UserDiscount userDiscount = new UserDiscount()
            {
                UserId = order.UserId,
                DiscountId = discount.DiscountId
            };
            AddUserDiscount(userDiscount);
            return DiscountUseType.Success;
        }
    }
}
