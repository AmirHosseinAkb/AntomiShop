using Antomi.Data.Entities.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Antomi.Data.Entities.Order
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }

        [Required]
        public int UserId { get; set; }
        public int? AddressId { get; set; }

        [Required]
        public int OrderSum { get; set; } = 0;

        [Required]
        public int DiscountPrice { get; set; } = 0;

        [Required]
        public int PaidPrice { get; set; } = 0;

        [Display(Name = "نوع پرداخت")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد")]
        public string PaymentKind { get; set; } = "";

        [Display(Name = "وضعیت پرداخت")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد")]
        public string PaymentStatus { get; set; } = "پرداخت نشده";
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public bool IsFinally { get; set; }
        public bool IsDeleted { get; set; }

        #region Relations

        public List<OrderDetail> OrderDetails { get; set; }
        public User.User User { get; set; }
        public Address Address { get; set; }

        #endregion
    }
}
