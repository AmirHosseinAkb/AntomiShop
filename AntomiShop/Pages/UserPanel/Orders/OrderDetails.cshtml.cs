using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Antomi.Core.Services.Interfaces;
using Antomi.Data.Entities.Order;
using Microsoft.AspNetCore.Authorization;
using Antomi.Data.Entities.Wallet;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AntomiShop.Pages.UserPanel.Orders
{
    [Authorize]
    public class OrderDetailsModel : PageModel
    {
        private IOrderService _orderService;
        private IUserService _userService;
        public OrderDetailsModel(IOrderService orderService,IUserService userService)
        {
            _orderService = orderService;
            _userService = userService;
        }
        public Order Order { get; set; }
        public void OnGet(int orderId, string discountStatus = "",bool IsSucceededPay=false)
        {
            ViewData["DiscountStatus"] = discountStatus;
            ViewData["SuccessPay"] = IsSucceededPay;
            var addresses= _orderService.GetUserAddressesForSelectInOrder(User.Identity.Name);
            ViewData["UserAddresses"] = new SelectList(addresses, "Value", "Text");
            Order =_orderService.GetOrder(User.Identity.Name, orderId);
        }
        public IActionResult OnPost(int orderId,string paymentType,int addressId)
        {
            var userId = _userService.GetUserIdByEmail(User.Identity.Name);
            var order = _orderService.GetUserOrder(User.Identity.Name,orderId);
            if (order == null || order.IsFinally)
            {
                return NotFound();
            }
            if (paymentType == "OnlinePayment")
            {
                order.AddressId = addressId;
                _orderService.UpdateOrder(order);
                var payment = new ZarinpalSandbox.Payment(order.PaidPrice);
                var response = payment.PaymentRequest("پرداخت فاکتور خرید شماره " + order.OrderId, "http://localhost:5059/PayOnlineOrder/"+orderId);
                if (response.Result.Status == 100)
                {
                    return Redirect("https://SandBox.ZarinPal.com/pg/StartPay/" + response.Result.Authority);
                }
            }
            else
            {
                if (_userService.BalanceUserWallet(User.Identity.Name) < order.PaidPrice)
                {
                    return BadRequest();
                }
                else
                {
                    Wallet wallet = new Wallet()
                    {
                        TypeId = 2,
                        UserId = userId,
                        Amount = order.PaidPrice,
                        CreateDate = DateTime.Now,
                        Description = "پرداخت فاکتور شماره " + orderId,
                        IsFinalled = true
                    };
                    _userService.AddWallet(wallet);
                    order.IsFinally = true;
                    order.CreateDate = DateTime.Now;
                    order.PaymentKind = "پرداخت از طریق کیف پول";
                    order.PaymentStatus = "در انتظار";
                    order.AddressId = addressId;
                    _orderService.UpdateOrder(order);
                    
                    return Redirect("/UserPanel/Orders/OrderDetails/"+orderId+"?IsSucceededPay=true");
                }
            }
            return RedirectToPage("UserOrders");
        }

        public IActionResult OnPostUseDiscount(string code,int orderId)
        {
            var status=_orderService.UseDiscount(orderId, code);
            return Redirect("/UserPanel/Orders/OrderDetails/" + orderId+"?discountStatus="+status);
        }
    }
}
