﻿using Antomi.Core.Services.Interfaces;
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
using Antomi.Data.Entities.Discount;
using Antomi.Data.Entities.Wallet;
using Microsoft.AspNetCore.Mvc.Rendering;

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

        public void AcceptOrder(int orderId)
        {
            var order = GetOrderById(orderId);
            order.PaymentStatus = "ارسال شده";
            _context.SaveChanges();
        }

        public void AddDiscount(Discount discount)
        {
            _context.Discounts.Add(discount);
            _context.SaveChanges();
        }

        public void AddUserDiscount(UserDiscount userDiscount)
        {
            _context.UserDiscounts.Add(userDiscount);
            _context.SaveChanges();
        }

        public void DeleteDiscount(int discountId)
        {
            var discount = _context.Discounts.Find(discountId);
            discount.IsDeleted = true;
            _context.SaveChanges();
        }

        public List<Data.Entities.Discount.Discount> GetAllDiscountsForShowInAdmin()
        {
            return _context.Discounts.ToList();
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

        public List<OrderDetail> GetOrderDetails(int orderId)
        {
            return _context.OrderDetails
                .Include(d=>d.Product)
                .Include(d=>d.ProductColor)
                .Where(d => d.OrderId == orderId).ToList();
        }

        public Tuple<List<Order>,int,int> GetOrdersForShowInAdmin(int pageId=1,string filterName="")
        {
            IQueryable<Order> result = _context.Orders.Include(o => o.User).Include(o=>o.Address).Where(o=>o.IsFinally);
            if (!string.IsNullOrEmpty(filterName))
            {
                result = result.Where(o => o.User.FirstName.Contains(filterName) || o.User.LastName.Contains(filterName));
            }
            int take = 10;
            int skip = (pageId - 1) * take;
            int pageCount = result.Count() / take;
            if (result.Count() % take != 0)
            {
                pageCount++;
            }
            var query = result.Skip(skip).Take(take).ToList();
            return Tuple.Create(query,pageId,pageCount);
        }

        public List<SelectListItem> GetUserAddressesForSelectInOrder(string email)
        {
            var userId=_userService.GetUserIdByEmail(email);
            return _context.Addresses.Where(a => a.UserId == userId)
                .Select(a => new SelectListItem()
                {
                    Text = a.State + " " + a.City + " " + a.Neighborhood + " پلاک " + a.Number,
                    Value = a.AddressId.ToString()
                }).ToList();
        }

        public Order GetUserOrder(string email, int orderId)
        {
            var userId = _userService.GetUserIdByEmail(email);
            return _context.Orders.SingleOrDefault(o => o.OrderId == orderId && o.UserId == userId);
        }

        public List<Order> GetUserOrders(string email)
        {
            int userId = _userService.GetUserIdByEmail(email);
            return _context.Orders.Where(o => o.UserId == userId).ToList();
        }

        public bool IsExistDiscount(string code)
        {
            return _context.Discounts.Any(d => d.DiscountCode == code);
        }

        public void UpdateDiscount(Discount discount)
        {
            _context.Discounts.Update(discount);
            _context.SaveChanges();
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
            if (discount.StartDate!=null && discount.StartDate > DateTime.Now)
                return DiscountUseType.ExpireTime;
            if (discount.EndDate!=null && discount.EndDate< DateTime.Now)
                return DiscountUseType.ExpireTime;
            if (discount.UsableCount!=null && discount.UsableCount < 1)
                return DiscountUseType.Finished;
            if (_context.UserDiscounts.Any(ud => ud.DiscountId == discount.DiscountId && ud.UserId == order.UserId))
                return DiscountUseType.UserUsed;
            var discountPrice = order.OrderSum * discount.DiscountPercent/100;
            order.DiscountPrice = discountPrice;
            order.PaidPrice -= discountPrice;
            if (discount.UsableCount != null)
            {
                discount.UsableCount--;
            }
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
