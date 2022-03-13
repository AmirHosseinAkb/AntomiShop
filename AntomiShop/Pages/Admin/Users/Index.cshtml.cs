using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Antomi.Core.Services.Interfaces;
using Antomi.Core.DTOs.User;

namespace AntomiShop.Pages.Admin.Users
{
    public class IndexModel : PageModel
    {
        private IUserService _userService;
        public IndexModel(IUserService userService)
        {
            _userService = userService;
        }
        public ShowUsersInAdminViewModel ShowUsersInAdminViewModel { get; set; }
        public void OnGet(int pageId=1,string filterName="",string filterEmail="")
        {
            ShowUsersInAdminViewModel=_userService.GetUsersForShowInAdmin(pageId,filterName,filterEmail);
        }

        public IActionResult OnPostDeleteUser(int userId)
        {
            _userService.DeleteUser(userId);
            return RedirectToPage("Index");
        }
    }
}
