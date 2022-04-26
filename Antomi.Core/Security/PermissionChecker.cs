using Antomi.Core.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Antomi.Core.Security
{
    public class PermissionChecker : AuthorizeAttribute, IAuthorizationFilter
    {
        int _permissionId;
        private IPermissionService _permissionService;
        public PermissionChecker(int permissionId)
        {
            _permissionId = permissionId;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            _permissionService = (IPermissionService)context.HttpContext.RequestServices.GetService(typeof(IPermissionService));
            if (!context.HttpContext.User.Identity.IsAuthenticated)
            {
                new RedirectResult("/Login");
            } 
            else if (!_permissionService.IsUserHasPermission(context.HttpContext.User.Identity.Name, _permissionId))
            {
                new RedirectResult("/Login");
            }
        }
    }
}
