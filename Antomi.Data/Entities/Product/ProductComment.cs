using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Antomi.Data.Entities.Product
{
    public class ProductComment
    {
        [Key]
        public int CommentId { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public int ProductId { get; set; }
        [Display(Name = "")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد")]
        public string Comment { get; set; }
        [Required]
        public DateTime CreateDate { get; set; }
        public bool IsAdminAccepted { get; set; }
        public bool IsDeleted { get; set; }

        #region Relations

        public Product Product { get; set; }
        public User.User User { get; set; }

        #endregion
    }
}
