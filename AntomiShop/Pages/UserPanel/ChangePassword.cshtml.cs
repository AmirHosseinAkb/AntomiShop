using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Antomi.Core.Services.Interfaces;
using Antomi.Core.DTOs.User;
using Antomi.Core.Security;
using Microsoft.AspNetCore.Authorization;

namespace AntomiShop.Pages.UserPanel
{
    [Authorize]
    public class ChangePasswordModel : PageModel
    {
        private IUserService _userService;
        public ChangePasswordModel(IUserService userService)
        {
            _userService = userService;
        }
        [BindProperty]
        public ChangePasswordViewModel change { get; set; }
        public void OnGet()
        {

        }
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var user = _userService.GetUserByEmail(User.Identity.Name);
            if (PasswordHasher.HashPasswordMD5(change.CurrentPassword) != user.Password)
            {
                ViewData["PasswordNotMatched"] = true;
                return Page();
            }
            if (change.NewPassword != change.RepeatNewPassword)
            {
                ViewData["NotMatched"] = true;
                return Page();
            }
            //Change Password
            _userService.ChangePassword(User.Identity.Name,change.NewPassword);
            ViewData["PasswordChanged"] = true;
            return Page();
        }
    }
}
