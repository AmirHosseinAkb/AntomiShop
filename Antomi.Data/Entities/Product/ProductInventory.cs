using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Antomi.Data.Entities.Product
{
    public class ProductInventory
    {
        [Key,ForeignKey("Product")]
        public int ProductId { get; set; }

        [Display(Name = "موجودی کالا")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int ProductCount { get; set; }

        public Product Product { get; set; }

    }
}
