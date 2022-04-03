using Antomi.Core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antomi.Data.Context;
using Antomi.Data.Entities.Order;

namespace Antomi.Core.Services
{
    public class OrderService:IOrderService
    {
        private AntomiContext _context;
        private IUserService _userService;
        public OrderService(AntomiContext context,IUserService userService)
        {
            _context= context;
            _userService= userService;
        }

        public List<Order> GetUserOrders(string email)
        {
            int userId=_userService.GetUserIdByEmail(email);
            return _context.Orders.Where(o => o.UserId == userId).ToList();
        }
    }
}
