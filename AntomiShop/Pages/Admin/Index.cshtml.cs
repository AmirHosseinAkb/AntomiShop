using Antomi.Core.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AntomiShop.Pages.Admin
{
    [Authorize]
    [PermissionChecker(new int[] { 1 })]
    public class IndexModel : PageModel
    {
        public void OnGet()
        {

        }
    }
}
