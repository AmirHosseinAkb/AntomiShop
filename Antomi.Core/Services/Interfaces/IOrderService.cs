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
    }
}
