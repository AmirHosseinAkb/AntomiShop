using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antomi.Core.Services.Interfaces;
using Antomi.Data.Context;
using Antomi.Data.Entities.User;

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

        public List<Role> GetAllRoles()
        {
            return _context.Roles.ToList();
        }
    }
}
