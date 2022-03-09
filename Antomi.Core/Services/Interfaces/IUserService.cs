﻿using Antomi.Core.DTOs.User;
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

        #region UserPanel

        UserPanelInformationsViewModel GetUserPanelInformations(string email);
        ConfirmUserInformationsViewModel GetUserInformationsToConfirm(string email);

        #endregion
    }
}
