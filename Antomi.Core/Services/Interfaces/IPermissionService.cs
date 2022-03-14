using Antomi.Data.Entities.Permission;
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
        List<Role> GetRolesForShowInAdmin();
        List<Permission> GetAllPermissions();
        int AddRole(Role role);
        void AddRolePermissions(int roleId, List<int> permissionIds);
        Role GetRoleById(int roleId);
        List<int> GetRolePermissions(int roleId);
        void UpdateRole(Role role);
        void EditRolePermissions(int roleId, List<int> permissionIds);
        void DeleteRole(int roleId);
    }
}
