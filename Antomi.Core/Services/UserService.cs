using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antomi.Core.Convertors;
using Antomi.Core.DTOs.User;
using Antomi.Core.Generators;
using Antomi.Core.Security;
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

        public bool ActiveUserAccount(string activeCode)
        {
            var user = _context.Users.SingleOrDefault(u => u.ActiveCode == activeCode);
            if (user != null)
            {
                user.IsActive = true;
                user.ActiveCode = NameGenerator.GenerateUniqName();
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public int AddUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            return user.UserId;
        }

        public User GetUserByEmail(string email)
        {
            return _context.Users.SingleOrDefault(u => u.Email == EmailConvertor.FixEmail(email));
        }

        public User GetUserForLogin(string email, string password)
        {
            return _context.Users
                .SingleOrDefault(u => u.Email == EmailConvertor.FixEmail(email) && u.Password == PasswordHasher.HashPasswordMD5(password));
        }

        public UserPanelInformationsViewModel GetUserPanelInformations(string email)
        {
            var user = GetUserByEmail(email);
            UserPanelInformationsViewModel informations = new UserPanelInformationsViewModel()
            {
                FullName = user.FirstName + " " + user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                RegisterDate = user.RegisterDate,
                WalletBalance=0
            };
            return informations;
        }

        public bool IsExistEmail(string email)
        {
            return _context.Users.Any(u=>u.Email == EmailConvertor.FixEmail(email));
        }
    }
}
