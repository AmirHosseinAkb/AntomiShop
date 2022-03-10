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
        [Display(Name = "استان")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد")]
        public string State { get; set; }

        [Display(Name = "شهر")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد")]
        public string City { get; set; }

        [Display(Name = "خیابان و کوچه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد")]
        public string Neighborhood { get; set; }

        [Display(Name = "پلاک")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int Number { get; set; }
        #region Relation

        public User? User { get; set; }

        #endregion
    }
}
