using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Antomi.Data.Entities.User
{
    public class Address
    {
        [Key]
        public int AddressId { get; set; }
        [Required]
        public int UserId { get; set; }
        [Display(Name = "آدرس")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(700, ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد")]
        public string AddressTitle { get; set; }

        #region Relation

        public User User { get; set; }

        #endregion
    }
}
