using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Antomi.Core.Services.Interfaces;
using Antomi.Data.Entities.User;

namespace AntomiShop.Pages.UserPanel
{
    public class UserAddressesModel : PageModel
    {
        private IUserService _userService;
        public UserAddressesModel(IUserService userService)
        {
            _userService = userService;
        }
        public Address Address { get; set; }
        public void OnGet()
        {
            ViewData["Addresses"]=_userService.GetUserAdresses(User.Identity.Name);
        }
        public IActionResult OnGetCreateAddress()
        {
            return new PartialViewResult()
            {
                ViewName = "_CreateAddress"
            };
        }
        public IActionResult OnPostCreateAddress()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            return Redirect("/UserPanel/UserAddersses");
        }
    }
}
