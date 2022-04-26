using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Antomi.Core.Services.Interfaces;
using Antomi.Data.Entities.User;
using Antomi.Core.DTOs.User;
using Antomi.Data.Entities.User;
using Antomi.Core.Convertors;
using Antomi.Core.Security;
using Antomi.Core.Generators;
using Microsoft.AspNetCore.Authorization;

namespace AntomiShop.Pages.Admin.Users
{
    [Authorize]
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

        public IActionResult OnPost()
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
            _userService.AddUserFromAdmin(CreateUserViewModel);

            return RedirectToPage("Index");
        }
    }
}
