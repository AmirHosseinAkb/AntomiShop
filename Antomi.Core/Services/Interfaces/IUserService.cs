using Antomi.Core.DTOs.User;
using Antomi.Data.Entities.User;
using Antomi.Data.Entities.Wallet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Antomi.Core.Services.Interfaces
{
    public interface IUserService
    {
        #region User
        bool IsExistEmail(string email);
        int AddUser(User user);
        bool ActiveUserAccount(string activeCode);
        User GetUserForLogin(string email, string password);
        User GetUserByEmail(string email);
        User GetUserById(int userId);
        int GetUserIdByEmail(string email);
        #endregion

        #region UserPanel

        UserPanelInformationsViewModel GetUserPanelInformations(string email);
        ConfirmUserDetailsViewModel GetUserDetailsToConfirm(string email);
        void ConfirmUserDetails(ConfirmUserDetailsViewModel details);
        void ChangePassword(string email, string password);
        int AddWallet(Wallet wallet);
        List<Wallet> GetUserWallets(string email);
        Wallet GetWalletById(int walletId);
        void UpdateWallet(Wallet wallet);
        int BalanceUserWallet(string email);

        #endregion

        #region Address

        List<Address> GetUserAdresses(string email);
        void AddAddress(Address address);
        void DeleteAddress(int addressId);

        #endregion

        #region Admin

        int AddUserFromAdmin(CreateUserViewModel create);
        ShowUsersInAdminViewModel GetUsersForShowInAdmin(int pageId = 1, string name = "", string email = "");

        #endregion
    }
}
