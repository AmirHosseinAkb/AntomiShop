using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Antomi.Core.Services.Interfaces;
using Antomi.Data.Entities.User;
using Microsoft.AspNetCore.Authorization;
using Antomi.Core.Security;

namespace AntomiShop.Pages.Admin.Roles
{
    [Authorize]
    [PermissionChecker(9|10|11|12)]
    public class IndexModel : PageModel
    {
        private IUserService _userService;
        private IPermissionService _permissionService;
        public IndexModel(IUserService userService,IPermissionService permissionService)
        {
            _userService = userService;
            _permissionService = permissionService;
        }
        public List<Role> Roles { get; set; }
        public void OnGet()
        {
            Roles = _permissionService.GetRolesForShowInAdmin();
        }

        public IActionResult OnPostDeleteRole(int roleId)
        {
            _permissionService.DeleteRole(roleId);
            return RedirectToPage("Index");
        }
    }
}
