using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Antomi.Core.Extensions
{
    public static class ClaimPrincipalExtension
    {
        public static string GetAvatarName(this ClaimsPrincipal user)
            => user.FindFirst("AvatarName").Value;
    }
}
