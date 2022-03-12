using Antomi.Data.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Antomi.Core.Services.Interfaces
{
    public interface IPermissionService
    {
        List<Role> GetAllRoles();
        void AddUserRoles(int userId, List<int> roleIds);
        List<int> GetUserRoles(int userId);
        void EditUserRoles(int userId, List<int> roleIds);
    }
}
