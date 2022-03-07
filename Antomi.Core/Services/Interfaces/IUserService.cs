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
    }
}
