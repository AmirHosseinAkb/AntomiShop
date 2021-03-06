using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Antomi.Core.Services.Interfaces;
using Antomi.Core.DTOs.User;
using Microsoft.AspNetCore.Authorization;
using Antomi.Core.Security;

namespace AntomiShop.Pages.Admin.Users
{
    [Authorize]
    [PermissionChecker(new int[] {4,8})]
    public class DeletedUsersListModel : PageModel
    {
        private IUserService _userService;
        public DeletedUsersListModel(IUserService userService)
        {
            _userService = userService;
        }
        public ShowUsersInAdminViewModel ShowUsersInAdminViewModel { get; set; }
        public void OnGet(int pageId=1,string filterName="",string filterEmail="")
        {
            ShowUsersInAdminViewModel = _userService.GetDeletedUsersForShowInAdmin(pageId, filterName, filterEmail);
        }

        public IActionResult OnPostReturnUser(int userId)
        {
            _userService.ReturnDeletedUser(userId);
            return RedirectToPage("DeletedUsersList");
        }
    }
}
