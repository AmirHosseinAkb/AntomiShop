using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Antomi.Core.DTOs.User
{
    public class RegisterViewModel
    {
        [Display(Name = "ایمیل")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد")]
        [MinLength(5, ErrorMessage = "{0} نمیتواند کمتر از {1} کاراکتر باشد")]
        [EmailAddress(ErrorMessage = "فرمت ایمیل صحیح نمی باشد")]
        public string Email { get; set; }

        [Display(Name = "رمز عبور")]
        [MaxLength(50, ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد")]
        [MinLength(8, ErrorMessage = "{0} نمیتواند کمتر از {1} کاراکتر باشد")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "تکرار رمز عبور")]
        [MaxLength(50, ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد")]
        [MinLength(8, ErrorMessage = "{0} نمیتواند کمتر از {1} کاراکتر باشد")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "تکرار رمز عبور صحیح نمی باشد")]
        public string Repassword { get; set; }
    }
}
