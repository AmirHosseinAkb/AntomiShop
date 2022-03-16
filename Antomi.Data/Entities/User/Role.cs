using Antomi.Data.Entities.Permission;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Antomi.Data.Entities.User
{
    public class Role
    {
        [Key]
        public int RoleId { get; set; }

        [Display(Name = "عنوان نقش")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد")]
        public string RoleTitle { get; set; }
        public bool IsDeleted { get; set; }

        #region Relations
        public List<RolePermission>? RolePermissions { get; set; }
        public List<User>? Users { get; set; }
        #endregion
    }
}
