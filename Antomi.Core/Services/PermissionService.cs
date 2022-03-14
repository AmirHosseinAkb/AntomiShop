using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antomi.Core.Services.Interfaces;
using Antomi.Data.Context;
using Antomi.Data.Entities.Permission;
using Antomi.Data.Entities.User;
using Microsoft.EntityFrameworkCore;

namespace Antomi.Core.Services
{
    public class PermissionService : IPermissionService
    {
        private AntomiContext _context;
        public PermissionService(AntomiContext context)
        {
            _context = context;
        }

        public int AddRole(Role role)
        {
            _context.Roles.Add(role);
            _context.SaveChanges();
            return role.RoleId;
        }

        public void AddRolePermissions(int roleId, List<int> permissionIds)
        {
            foreach (int permissionId in permissionIds)
            {
                _context.RolePermissions.Add(new RolePermission()
                {
                    RoleId=roleId,
                    PermissionId=permissionId
                });
            }
            _context.SaveChanges();
        }

        public void AddUserRoles(int userId, List<int> roleIds)
        {
            foreach (int roleId in roleIds)
            {
                _context.UserRoles.Add(new UserRoles()
                {
                    UserId = userId,
                    RoleId = roleId
                });
            }
            _context.SaveChanges();
        }

        public void EditRolePermissions(int roleId, List<int> permissionIds)
        {
            _context.RolePermissions.Where(rp => rp.RoleId == roleId).ToList().ForEach(rp => _context.RolePermissions.Remove(rp));
            AddRolePermissions(roleId, permissionIds);
        }

        public void EditUserRoles(int userId, List<int> roleIds)
        {
            _context.UserRoles.Where(ur => ur.UserId == userId).ToList().ForEach(ur => _context.UserRoles.Remove(ur));
            AddUserRoles(userId, roleIds);
        }

        public List<Permission> GetAllPermissions()
        {
            return _context.Permissions.ToList();
        }

        public List<Role> GetAllRoles()
        {
            return _context.Roles.ToList();
        }

        public Role GetRoleById(int roleId)
        {
            return _context.Roles.Find(roleId);
        }

        public List<int> GetRolePermissions(int roleId)
        {
            return _context.RolePermissions.Where(rp => rp.RoleId==roleId).Select(rp=>rp.PermissionId).ToList();
        }

        public List<Role> GetRolesForShowInAdmin()
        {
            return _context.Roles
                .Include(r => r.RolePermissions)
                 .ThenInclude(rp=>rp.Permission)
                .ToList();
        }

        public List<int> GetUserRoles(int userId)
        {
            return _context.UserRoles.Where(ur => ur.UserId == userId).Select(ur => ur.RoleId).ToList();
        }

        public void UpdateRole(Role role)
        {
            _context.Roles.Update(role);
            _context.SaveChanges();
        }
    }
}
