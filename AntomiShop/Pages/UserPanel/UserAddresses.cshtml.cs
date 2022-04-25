using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Antomi.Core.Services.Interfaces;
using Antomi.Data.Entities.User;
using Microsoft.AspNetCore.Authorization;

namespace AntomiShop.Pages.UserPanel
{
    [Authorize]
    public class UserAddressesModel : PageModel
    {
        private IUserService _userService;
        public UserAddressesModel(IUserService userService)
        {
            _userService = userService;
        }
        [BindProperty]
        public Address Address { get; set; }
        public void OnGet(bool UserHasntAddress=false)
        {
            ViewData["UserHasntAddress"] = UserHasntAddress;
            ViewData["Addresses"]=_userService.GetUserAdresses(User.Identity.Name);
        }
        public IActionResult OnPostCreateAddress()
        {
            if (!ModelState.IsValid)
            {
                ViewData["Addresses"] = _userService.GetUserAdresses(User.Identity.Name);
                return Page();
            }
            var userId = _userService.GetUserIdByEmail(User.Identity.Name);
            Address address = new Address()
            {
                UserId = userId,
                State = Address.State,
                City = Address.City,
                Neighborhood = Address.Neighborhood,
                Number = Address.Number
            };
            _userService.AddAddress(address);
            return Redirect("/UserPanel/UserAddresses");
        }

        public IActionResult OnPostDeleteAddress(int addressId)
        {
            _userService.DeleteAddress(addressId);
            return RedirectToPage("UserAddresses");
        }
    }
}
