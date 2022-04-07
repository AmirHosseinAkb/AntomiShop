using Antomi.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AntomiShop.Controllers
{
    public class HomeController : Controller
    {
        private IUserService _userService;
        private IOrderService _orderService;
        public HomeController(IUserService userService,IOrderService orderService)
        {
            _userService = userService;
            _orderService = orderService; 
        }

        public IActionResult Index()
        {
            return View();
        }

        [Route("OnlinePayment/{walletId}")]
        public IActionResult OnlinePayment(int walletId)
        {
            if (HttpContext.Request.Query["Status"] != ""
                && HttpContext.Request.Query["Status"].ToString().ToLower() == "ok"
                && HttpContext.Request.Query["Authority"] != "")
            {
                var authority = HttpContext.Request.Query["Authority"];
                var wallet = _userService.GetWalletById(walletId);
                var payment = new ZarinpalSandbox.Payment(wallet.Amount);
                var response = payment.Verification(authority).Result;
                if (response.Status == 100)
                {
                    wallet.IsFinalled = true;
                    ViewBag.IsSucceed = true;
                    ViewBag.Code = response.RefId;
                    _userService.UpdateWallet(wallet);
                    return View();
                }
            }
            return View();
        }

        [Route("file-upload")]
        public IActionResult UploadImage(IFormFile upload, string CKEditorFuncNum, string CKEditor, string langCode)
        {
            if (upload.Length <= 0) return null;

            var fileName = Guid.NewGuid() + Path.GetExtension(upload.FileName).ToLower();
            var path = Path.Combine(
                Directory.GetCurrentDirectory(), "wwwroot/MyImages",
                fileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                upload.CopyTo(stream);
            }

            var url = $"{"/MyImages/"}{fileName}";

            return Json(new { uploaded = true, url });
        }
        [Route("PayOnlineOrder/{orderId}")]
        public IActionResult PayOnlineOrder(int orderId)
        {
            if (HttpContext.Request.Query["Status"] != ""
                && HttpContext.Request.Query["Status"].ToString().ToLower() == "ok"
                && HttpContext.Request.Query["Authority"] != "")
            {
                var authority = HttpContext.Request.Query["Authority"];
                var order = _orderService.GetOrderById(orderId);
                var payment = new ZarinpalSandbox.Payment(order.PaidPrice);
                var response = payment.Verification(authority).Result;
                if (response.Status == 100)
                {
                    order.IsFinally = true;
                    order.PaymentKind = "پرداخت انلاین";
                    order.PaymentStatus = "در انتظار";
                    ViewBag.IsSucceed = true;
                    ViewBag.Code = response.RefId;
                    _orderService.UpdateOrder(order);
                    return View("OnlinePayment");
                }
            }
            return View("OnlinePayment");
        }
    }
}
