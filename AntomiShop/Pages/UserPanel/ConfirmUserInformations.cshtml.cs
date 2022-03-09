using Antomi.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AntomiShop.Pages.UserPanel
{
    public class ConfirmUserInformationsModel : PageModel
    {
        private IUserService _userService;
        public ConfirmUserInformationsModel(IUserService userService)
        {
            _userService = userService;
        }
        public int MyProperty { get; set; }
        public void OnGet()
        {

        }
    }
}
