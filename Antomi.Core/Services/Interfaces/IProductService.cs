using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Antomi.Core.Services.Interfaces
{
    public interface IProductService
    {
        List<SelectListItem> GetGroupsForSelect();
        List<SelectListItem> GetSubGroupsOfGroups(int groupId);
    }
}
