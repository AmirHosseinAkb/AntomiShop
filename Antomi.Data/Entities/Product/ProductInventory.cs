using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Antomi.Data.Entities.Product
{
    public class ProductInventory
    {
        [Key]
        public int InventoryId { get; set; }
        [Required]
        public int ProductId { get; set; }
        [Display(Name = "موجودی کالا")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int ProductCount { get; set; }
        public DateTime ChargeDate { get; set; }=DateTime.Now;

        public Product Product { get; set; }

    }
}
