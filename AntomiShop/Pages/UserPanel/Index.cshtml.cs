using Antomi.Data.Entities.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Antomi.Core.DTOs.User;
using Antomi.Core.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Globalization;

namespace AntomiShop.Pages.UserPanel
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private IUserService _userService;
        public IndexModel(IUserService userService)
        {
            _userService = userService;
        }
        [BindProperty]
        public ConfirmUserDetailsViewModel details { get; set; }
        public void OnGet()
        {
            details = _userService.GetUserDetailsToConfirm(User.Identity.Name);
        }
        public IActionResult OnPost(string birthDay)
        {
            if(birthDay == null)
            {
                ViewData["NullBirthday"] = true;
                return Page();
            }
            else
            {
                string[] splitBirthDay=birthDay.Split('/');
                details.BirthDay = new DateTime(int.Parse(splitBirthDay[0]),
                    int.Parse(splitBirthDay[1]),
                    int.Parse(splitBirthDay[2]),
                    new PersianCalendar());
            }
            //Confirm User Details
            _userService.ConfirmUserDetails(details);

            return Redirect("http://localhost:5059/UserPanel");
        }
    }
}
