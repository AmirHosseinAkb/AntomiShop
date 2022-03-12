using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Antomi.Core.Services.Interfaces;
using Antomi.Data.Entities.User;
using Antomi.Core.DTOs.User;
using Antomi.Data.Entities.User;
using Antomi.Core.Convertors;
using Antomi.Core.Security;
using Antomi.Core.Generators;

namespace AntomiShop.Pages.Admin.Users
{
    public class CreateUserModel : PageModel
    {
        private IUserService _userService;
        private IPermissionService _permissionService;
        public CreateUserModel(IUserService userService,IPermissionService permissionService)
        {
            _userService = userService;
            _permissionService = permissionService;
        }
        [BindProperty]
        public CreateUserViewModel CreateUserViewModel { get; set; }
        public void OnGet()
        {
            ViewData["Roles"] = _permissionService.GetAllRoles();
        }

        public IActionResult OnPost(List<int> selectedRoles)
        {
            if (!ModelState.IsValid)
            {
                ViewData["Roles"] = _permissionService.GetAllRoles();
                return Page();
            }
            if (_userService.IsExistEmail(CreateUserViewModel.Email))
            {
                ViewData["IsExistEmail"] = true;
                ViewData["Roles"] = _permissionService.GetAllRoles();
                return Page();
            }
            //Add User
            int userId=_userService.AddUserFromAdmin(CreateUserViewModel);
            //Add User Roles
            _permissionService.AddUserRoles(userId, selectedRoles);

            return RedirectToPage("Index");
        }
    }
}
