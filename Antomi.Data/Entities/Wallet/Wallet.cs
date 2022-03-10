using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Antomi.Data.Entities.Wallet
{
    public class Wallet
    {
        [Key]
        public int WalletId { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public int TypeId { get; set; }
        public int Amount { get; set; }
        [MaxLength(700)]
        public string Description { get; set; }
        public bool IsFinalled { get; set; }
        public DateTime CreateDate { get; set; }=DateTime.Now;

        #region Relations
        [ForeignKey("TypeId")]
        public WalletType WalletType { get; set; }
        public User.User User { get; set; }

        #endregion
    }
}
