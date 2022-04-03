using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Antomi.Data.Entities.Order
{
    public class OrderDetail
    {
        [Key]
        public int DetailId { get; set; }
        [Required]
        public int ProductId { get; set; }
        [Required]
        public int OrderId { get; set; }
        [Required]
        public int ColorId { get; set; }
        [Required]
        public int UnitPrice { get; set; }
        [Required]
        public int Count { get; set; }

        #region Relations

        public Order Order { get; set; }
        public Product.Product Product { get; set; }
        [ForeignKey("ColorId")]
        public Product.ProductColor ProductColor { get; set; }
        #endregion
    }
}
