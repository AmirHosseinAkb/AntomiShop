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
using Microsoft.EntityFrameworkCore;

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
                WalletBalance = BalanceUserWallet(email)
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
            _context.SaveChanges();
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

        public Wallet GetWalletById(int walletId)
        {
            return _context.Wallets.Find(walletId);
        }

        public void UpdateWallet(Wallet wallet)
        {
            _context.Wallets.Update(wallet);
            _context.SaveChanges();
        }

        public int BalanceUserWallet(string email)
        {
            var userId = GetUserIdByEmail(email);
            var deposite = _context.Wallets.Where(w => w.TypeId == 1 && w.IsFinalled && w.UserId == userId).ToList();
            var withdraw = _context.Wallets.Where(w => w.TypeId == 2 && w.IsFinalled && w.UserId == userId).ToList();
            return deposite.Sum(w => w.Amount) - withdraw.Sum(w => w.Amount);
        }

        public List<Address> GetUserAdresses(string email)
        {
            var userId = GetUserIdByEmail(email);
            return _context.Addresses.Where(a => a.UserId == userId).ToList();
        }

        public void AddAddress(Address address)
        {
            _context.Addresses.Add(address);
            _context.SaveChanges();
        }

        public void DeleteAddress(int addressId)
        {
            var address = _context.Addresses.Find(addressId);
            _context.Addresses.Remove(address);
            _context.SaveChanges();
        }

        public int AddUserFromAdmin(CreateUserViewModel create)
        {
            var user = new User()
            {
                RoleId=create.RoleId,
                Email = EmailConvertor.FixEmail(create.Email),
                Password = PasswordHasher.HashPasswordMD5(create.Password),
                ActiveCode = NameGenerator.GenerateUniqName(),
                IsActive = true,
                AvatarName="Default.png"
            };
            if (create.UserAvatar != null)
            {
                user.AvatarName = NameGenerator.GenerateUniqName() + Path.GetExtension(create.UserAvatar.FileName);
                string imagePath = Path.Combine(Directory.GetCurrentDirectory(),
                    "wwwroot",
                    "UserAvatar",
                    user.AvatarName);
                using(var stream=new FileStream(imagePath, FileMode.Create))
                {
                    create.UserAvatar.CopyTo(stream);
                }
            }
            AddUser(user);
            return user.UserId;
        }

        public ShowUsersInAdminViewModel GetUsersForShowInAdmin(int pageId = 1, string name = "", string email = "")
        {
            IQueryable<User> result = _context.Users.Include(ur => ur.Role);
            if (!string.IsNullOrEmpty(name))
            {
                result = result.Where(u => u.FirstName.Contains(name) || u.LastName.Contains(name));
            }
            if (!string.IsNullOrEmpty(email))
            {
                result = result.Where(u => u.Email.Contains(email));
            }
            int take = 1;
            int skip = (pageId - 1) * take;
            var showUsersVM = new ShowUsersInAdminViewModel()
            {
                Users = result.Skip(skip).Take(take).ToList(),
                CurrentPage = pageId,
                PageCount = result.Count() / take
            };
            if (result.Count() % take != 0)
            {
                showUsersVM.PageCount++;
            }
            return showUsersVM;
        }

        public EditUserViewModel GetUserForEdit(int userId)
        {
            return _context.Users.Where(u => u.UserId == userId)
                .Select(u => new EditUserViewModel()
                {
                    UserId=u.UserId,
                    RoleId=u.RoleId,
                    Email = u.Email,
                    AvatarName = u.AvatarName
                }).Single();
        }

        public void EditUserFormAdmin(EditUserViewModel editUserVM)
        {
            var user = GetUserById(editUserVM.UserId);
            user.RoleId = editUserVM.RoleId;
            user.Email = EmailConvertor.FixEmail(editUserVM.Email);
            if (!string.IsNullOrEmpty(editUserVM.Password))
            {
                user.Password = PasswordHasher.HashPasswordMD5(editUserVM.Password);
            }
            if (editUserVM.UserAvatar != null)
            {
                string imagePath = "";
                if (editUserVM.AvatarName != "Default.png")
                {
                    imagePath = Path.Combine(Directory.GetCurrentDirectory(),
                        "wwwroot",
                        "UserAvatar",
                        editUserVM.AvatarName);
                    if (File.Exists(imagePath))
                    {
                        File.Delete(imagePath);
                    }
                }
                user.AvatarName = NameGenerator.GenerateUniqName() + Path.GetExtension(editUserVM.UserAvatar.FileName);
                imagePath = Path.Combine(Directory.GetCurrentDirectory(),
                        "wwwroot",
                        "UserAvatar",
                        user.AvatarName);
                using(var stream=new FileStream(imagePath, FileMode.Create))
                {
                    editUserVM.UserAvatar.CopyTo(stream);
                }
            }
            _context.SaveChanges();
        }

        public void DeleteUser(int userId)
        {
            var user = GetUserById(userId);
            user.IsDeleted = true;
            _context.SaveChanges();
        }

        public ShowUsersInAdminViewModel GetDeletedUsersForShowInAdmin(int pageId = 1, string name = "", string email = "")
        {
            IQueryable<User> result = _context.Users.IgnoreQueryFilters().Where(u=>u.IsDeleted);
            if (!string.IsNullOrEmpty(name))
            {
                result = result.Where(u => u.FirstName.Contains(name) || u.LastName.Contains(name));
            }
            if (!string.IsNullOrEmpty(email))
            {
                result = result.Where(u => u.Email.Contains(email));
            }
            int take = 1;
            int skip = (pageId - 1) * take;
            var showUsersVM = new ShowUsersInAdminViewModel()
            {
                Users = result.Skip(skip).Take(take).ToList(),
                CurrentPage = pageId,
                PageCount = result.Count() / take
            };
            if (result.Count() % take != 0)
            {
                showUsersVM.PageCount++;
            }
            return showUsersVM;
        }

        public void ReturnDeletedUser(int userId)
        {
            var user = _context.Users.IgnoreQueryFilters().Where(u => u.UserId == userId).Single();
            user.IsDeleted = false;
            _context.SaveChanges();
        }

        //public void ArrUserRole(UserRole userRole)
        //{
        //    _context.UserRoles.Add(userRole);
        //    _context.SaveChanges();
        //}
    }
}
