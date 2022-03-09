using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Antomi.Core.DTOs.User
{
    public class UserPanelInformationsViewModel
    {
        public string? FullName { get; set; }
        public string Email { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime RegisterDate { get; set; }
        public int WalletBalance { get; set; }

    }
}
