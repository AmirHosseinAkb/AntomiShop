using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Antomi.Core.Services.Interfaces;
using Antomi.Core.DTOs.User;
using Antomi.Data.Entities.Wallet;

namespace AntomiShop.Pages.UserPanel
{
    public class WalletModel : PageModel
    {
        private IUserService _userService;
        public WalletModel(IUserService userService)
        {
            _userService = userService;
        }
        [BindProperty]
        public ChargeWalletViewMode Charge { get; set; }
        public void OnGet()
        {
            ViewData["UserWallets"] = _userService.GetUserWallets(User.Identity.Name);
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            if (Charge.Amount < 5000)
            {
                ViewData["MaximumAmount"] = true;
                return Page();
            }
            Wallet wallet = new Wallet()
            {
                TypeId = 1,
                Amount = Charge.Amount,
                Description = "شارژ کیف پول",
                IsFinalled = false,
                UserId = _userService.GetUserIdByEmail(User.Identity.Name)
            };
            int walletId = _userService.AddWallet(wallet);
            var payment = new ZarinpalSandbox.Payment(Charge.Amount);
            var response = payment.PaymentRequest("شارژ کیف پول", "http://localhost:5059/OnlinePayment/" + walletId);
            if (response.Result.Status == 100)
            {
                return Redirect("https://SandBox.Zarinpal.Com/pg/StartPay/" + response.Result.Authority);
            }
            return RedirectToPage("Wallet");
        }
    }
}
