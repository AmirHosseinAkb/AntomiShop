using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Antomi.Core.Services.Interfaces;
using Antomi.Data.Entities.User;

namespace AntomiShop.Pages.Admin.Roles
{
    public class CreateRoleModel : PageModel
    {
        private IPermissionService _permissionService;
        public CreateRoleModel(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }
        [BindProperty]
        public Role Role { get; set; }
        public void OnGet()
        {
            ViewData["Permissions"] = _permissionService.GetAllPermissions();
        }
        
        public IActionResult OnPost(List<int> selectedPermissions)
        {
            if (!ModelState.IsValid)
            {
                ViewData["Permissions"] = _permissionService.GetAllPermissions();
                return Page();
            }
            //Add Role
            int roleId=_permissionService.AddRole(Role);
            //Add Permissions To Role
            _permissionService.AddRolePermissions(roleId, selectedPermissions);
            return RedirectToPage("Index");
        }
    }
}
