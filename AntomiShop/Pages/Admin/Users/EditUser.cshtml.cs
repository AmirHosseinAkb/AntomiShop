using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Antomi.Core.Services.Interfaces;
using Antomi.Core.DTOs.User;
using Antomi.Core.Convertors;

namespace AntomiShop.Pages.Admin.Users
{
    public class EditUserModel : PageModel
    {
        private IUserService _userService;
        private IPermissionService _permissionService;
        public EditUserModel(IUserService userService,IPermissionService permissionService)
        {
            _userService = userService;
            _permissionService = permissionService;
        }
        public void GetInformation(int userId)
        {
            ViewData["Roles"] = _permissionService.GetAllRoles();
            ViewData["UserRoles"] = _permissionService.GetUserRoles(userId);
        }
        [BindProperty]
        public EditUserViewModel EditUserVM { get; set; }
        public void OnGet(int userId)
        {
            GetInformation(userId);
            EditUserVM=_userService.GetUserForEdit(userId);
        }

        public IActionResult OnPost(List<int> selectedRoles)
        {
            if (!ModelState.IsValid)
            {
                GetInformation(EditUserVM.UserId);
                return Page();
            }
            var user = _userService.GetUserById(EditUserVM.UserId);
            if (EmailConvertor.FixEmail(EditUserVM.Email) != user.Email)
            {
                if (_userService.IsExistEmail(EditUserVM.Email))
                {
                    GetInformation(EditUserVM.UserId);
                    ViewData["IsExistEmail"] = true;
                    return Page();
                }
            }
            //Edit User
            _userService.EditUserFormAdmin(EditUserVM);
            //Edit UserRoles
            _permissionService.EditUserRoles(EditUserVM.UserId, selectedRoles);
            return RedirectToPage("Index");
        }
    }
}
