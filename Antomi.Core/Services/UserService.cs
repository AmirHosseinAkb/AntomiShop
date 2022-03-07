using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antomi.Core.Convertors;
using Antomi.Core.Services.Interfaces;
using Antomi.Data.Context;
using Antomi.Data.Entities.User;

namespace Antomi.Core.Services
{
    public class UserService : IUserService
    {
        private AntomiContext _context;
        public UserService(AntomiContext context)
        {
            _context=context;
        }

        public int AddUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            return user.UserId;
        }

        public bool IsExistEmail(string email)
        {
            return _context.Users.Any(u=>u.Email == EmailConvertor.FixEmail(email));
        }
    }
}
