using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Antomi.Data.Entities.User
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Display(Name = "نام")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد")]
        [MinLength(3, ErrorMessage = "{0} نمیتواند کمتر از {1} کاراکتر باشد")]
        public string? FirstName { get; set; }

        [Display(Name = "نام خانوادگی")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد")]
        [MinLength(3, ErrorMessage = "{0} نمیتواند کمتر از {1} کاراکتر باشد")]
        public string? LastName { get; set; }

        [Display(Name = "ایمیل")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد")]
        [MinLength(5, ErrorMessage = "{0} نمیتواند کمتر از {1} کاراکتر باشد")]
        public string Email { get; set; }

        [Display(Name = "رمز عبور")]
        [MaxLength(50, ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد")]
        [MinLength(8, ErrorMessage = "{0} نمیتواند کمتر از {1} کاراکتر باشد")]
        public string Password { get; set; }
        [MaxLength(50)]
        public string ActiveCode { get; set; }
        [Display(Name = "شماره تلفن همراه")]
        [MaxLength(50, ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد")]
        public string? PhoneNumber { get; set; }
        public bool IsActive { get; set; }
        public DateTime? BirthDay { get; set; }
        public DateTime RegisterDate { get; set; } = DateTime.Now;
        [MaxLength(200)]
        public string AvatarName { get; set; }
        [Display(Name = "آدرس")]
        [MaxLength(700, ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد")]
        public string? Address { get; set; }
        public bool IsDeleted { get; set; }
    }
}
