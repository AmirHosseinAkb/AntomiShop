using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Antomi.Data.Entities.Product
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Required]
        public int GroupId { get; set; }

        public int? SubId { get; set; }

        public int? SecSubId { get; set; }

        [Display(Name = "عنوان محصول")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد")]
        public string ProductTitle { get; set; }

        [Display(Name = "توضیحات محصول")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(1000, ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد")]
        public string ProductDescription { get; set; }

        [Display(Name = "قیمت محصول")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int ProductPrice { get; set; }

        [Display(Name = "تصویر محصول")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد")]
        public string ProductImageName { get; set; } = "Product.png";

        [Display(Name = "کلمات کلیدی محصول")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(300, ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد")]
        public string ProductTags { get; set; }

        public DateTime CreateDate { get; set; } = DateTime.Now;
        public bool IsDeleted { get; set; }

        #region Relations

        [ForeignKey("GroupId")]
        public ProductGroup? ProductGroup { get; set; }
        [ForeignKey("SubId")]
        public ProductGroup? SubGroup { get; set; }
        [ForeignKey("SecSubId")]
        public ProductGroup? SecSubGroup { get; set; }
        public List<ProductImage>? ProductImages { get; set; }
        public ProductInventory ProductInventory { get; set; }
        public List<ProductColor>? ProductColors { get; set; }
        public List<Order.OrderDetail> OrderDetails { get; set; }
        public List<InventoryHistory> InventoryHistories { get; set; }
        public List<ProductComment> ProductComments { get; set; }
        public List<ProductDiscount> ProductDiscounts { get; set; }

        #endregion

    }
}
