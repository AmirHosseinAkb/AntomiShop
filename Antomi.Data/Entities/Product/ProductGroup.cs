using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Antomi.Data.Entities.Product
{
    public class ProductGroup
    {
        [Key]
        public int GroupId { get; set; }

        [Display(Name = "عنوان گروه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد")]
        public string GroupTitle { get; set; }

        public int? ParentId { get; set; }

        [ForeignKey("ParentId")]
        public List<ProductGroup> ProductGroups { get; set; }
        public bool IsDeleted { get; set; }

        #region Relations
        [InverseProperty("ProductGroup")]
        public List<Product> Products { get; set; }
        [InverseProperty("SubGroup")]
        public List<Product> SubProducts { get; set; }

        #endregion
    }
}
