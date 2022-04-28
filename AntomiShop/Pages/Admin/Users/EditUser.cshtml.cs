using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Antomi.Core.Services.Interfaces;
using Antomi.Core.DTOs.User;
using Antomi.Core.Convertors;
using Microsoft.AspNetCore.Authorization;
using Antomi.Core.Security;

namespace AntomiShop.Pages.Admin.Users
{
    [Authorize]
    [PermissionChecker(new int[] {4,6})]
    public class EditUserModel : PageModel
    {
        private IUserService _userService;
        private IPermissionService _permissionService;
        public EditUserModel(IUserService userService,IPermissionService permissionService)
        {
            _userService = userService;
            _permissionService = permissionService;
        }

            
        [BindProperty]
        public EditUserViewModel EditUserVM { get; set; }
        public void OnGet(int userId)
        {
            ViewData["Roles"] = _permissionService.GetAllRoles();
            EditUserVM =_userService.GetUserForEdit(userId);
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                ViewData["Roles"] = _permissionService.GetAllRoles();
                return Page();
            }
            var user = _userService.GetUserById(EditUserVM.UserId);
            if (EmailConvertor.FixEmail(EditUserVM.Email) != user.Email)
            {
                if (_userService.IsExistEmail(EditUserVM.Email))
                {
                    ViewData["Roles"] = _permissionService.GetAllRoles();
                    ViewData["IsExistEmail"] = true;
                    return Page();
                }
            }
            //Edit User
            _userService.EditUserFormAdmin(EditUserVM);

            return RedirectToPage("Index");
        }
    }
}
