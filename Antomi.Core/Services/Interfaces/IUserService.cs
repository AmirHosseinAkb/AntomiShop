using Antomi.Core.DTOs.User;
using Antomi.Data.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Antomi.Core.Services.Interfaces
{
    public interface IUserService
    {
        bool IsExistEmail(string email);
        int AddUser(User user);
        bool ActiveUserAccount(string activeCode);
        User GetUserForLogin(string email, string password);
        User GetUserByEmail(string email);
        User GetUserById(int userId);

        #region UserPanel

        UserPanelInformationsViewModel GetUserPanelInformations(string email);
        ConfirmUserDetailsViewModel GetUserDetailsToConfirm(string email);
        void ConfirmUserDetails(ConfirmUserDetailsViewModel details);
        void ChangePassword(string email, string password);

        #endregion
    }
}
