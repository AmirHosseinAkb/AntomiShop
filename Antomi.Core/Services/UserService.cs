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
using Antomi.Data.Entities.Wallet;

namespace Antomi.Core.Services
{
    public class UserService : IUserService
    {
        private AntomiContext _context;
        public UserService(AntomiContext context)
        {
            _context = context;
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

        public ConfirmUserDetailsViewModel GetUserDetailsToConfirm(string email)
        {
            var user = GetUserByEmail(email);
            var details = new ConfirmUserDetailsViewModel()
            {
                UserId = user.UserId,
                Email=user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                BirthDay = user.BirthDay,
                AvatarName = user.AvatarName
            };
            return details;
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
                WalletBalance = 0
            };
            return informations;
        }

        public bool IsExistEmail(string email)
        {
            return _context.Users.Any(u => u.Email == EmailConvertor.FixEmail(email));
        }

        public void ConfirmUserDetails(ConfirmUserDetailsViewModel details)
        {
            var user = GetUserById(details.UserId);

            if (details.UserAvatar != null)
            {
                string imagePath = "";
                if (details.AvatarName != "Default.png")
                {
                    imagePath = Path.Combine(Directory.GetCurrentDirectory(),
                        "wwwroot",
                        "UserAvatar",
                        details.AvatarName);
                    if (File.Exists(imagePath))
                    {
                        File.Delete(imagePath);
                    }
                }
                user.AvatarName = NameGenerator.GenerateUniqName() + Path.GetExtension(details.UserAvatar.FileName);
                imagePath = Path.Combine(Directory.GetCurrentDirectory(),
                        "wwwroot",
                        "UserAvatar",
                        user.AvatarName);
                using(var stream=new FileStream(imagePath, FileMode.Create))
                {
                    details.UserAvatar.CopyTo(stream);
                }
            }
            user.FirstName = details.FirstName;
            user.LastName = details.LastName;
            user.PhoneNumber = details.PhoneNumber;
            user.BirthDay = details.BirthDay;
            user.Email = details.Email;
            _context.SaveChanges();
        }

        public User GetUserById(int userId)
        {
            return _context.Users.Find(userId);
        }

        public void ChangePassword(string email, string password)
        {
            var user = GetUserByEmail(email);
            user.Password = PasswordHasher.HashPasswordMD5(password);
        }

        public int AddWallet(Wallet wallet)
        {
            _context.Wallets.Add(wallet);
            _context.SaveChanges();
            return wallet.WalletId;
        }

        public int GetUserIdByEmail(string email)
        {
            return GetUserByEmail(email).UserId;
        }

        public List<Wallet> GetUserWallets(string email)
        {
            var userId = GetUserIdByEmail(email);
            return _context.Wallets.Where(w => w.UserId == userId).ToList();
        }
    }
}
