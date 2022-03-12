using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Antomi.Core.Services.Interfaces;
using Antomi.Data.Entities.User;

namespace AntomiShop.Pages.Admin.Users
{
    public class CreateUserModel : PageModel
    {
        private IUserService _userService;
        public CreateUserModel(IUserService userService)
        {
            _userService = userService;
        }
        prop
        public void OnGet()
        {
        }
    }
}
