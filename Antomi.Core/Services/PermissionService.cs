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

        public List<Role> GetRolesForShowInAdmin()
        {
            return _context.Roles.Include(r => r.RolePermissions).ToList();
        }

        public List<int> GetUserRoles(int userId)
        {
            return _context.UserRoles.Where(ur => ur.UserId == userId).Select(ur => ur.RoleId).ToList();
        }
    }
}
