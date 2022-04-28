using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Antomi.Core.Services.Interfaces;
using Antomi.Data.Entities.User;
using Microsoft.AspNetCore.Authorization;
using Antomi.Core.Security;

namespace AntomiShop.Pages.Admin.Roles
{
    [Authorize]
    [PermissionChecker(new int[] { 9, 11 })]
    public class EditRoleModel : PageModel
    {
        
        private IPermissionService _permissionService;
        public EditRoleModel(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }
        [BindProperty]
        public Role Role { get; set; }
        public void OnGet(int roleId)
        {
            Role=_permissionService.GetRoleById(roleId);
            ViewData["Permissions"] = _permissionService.GetAllPermissions();
            ViewData["RolePermissions"] = _permissionService.GetRolePermissions(roleId);
        }
        public IActionResult OnPost(List<int> selectedPermissions)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            //Update Role
            _permissionService.UpdateRole(Role);
            //Edit Role Permissions
            _permissionService.EditRolePermissions(Role.RoleId, selectedPermissions);

            return RedirectToPage("Index");
        }
    }
}
