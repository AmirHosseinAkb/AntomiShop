﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Antomi.Core.DTOs.User
{
    public class ShowUsersInAdminViewModel
    {
        public List<Antomi.Data.Entities.User.User> Users { get; set; }
        public int CurrentPage { get; set; }
        public int PageCount { get; set; }
    }
    public class CreateUserViewModel
    {
        public int RoleId { get; set; }
        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد")]
        public string Email { get; set; }

        [Display(Name = "رمز عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public IFormFile? UserAvatar { get; set; }
    }
    
    public class EditUserViewModel
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }
        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد")]
        public string Email { get; set; }

        [Display(Name = "رمز عبور")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        public IFormFile? UserAvatar { get; set; }
        public string AvatarName { get; set; }
    }
}
