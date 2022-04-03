using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Antomi.Data.Entities.Product
{
    public class InventoryHistory
    {
        [Key]
        public int HistoryId { get; set; }
        [Required]
        public int ProductId { get; set; }
        [Required]
        public int Count { get; set; }
        [Required]
        public DateTime Date { get; set; }

        #region Relations

        public Product Product { get; set; }

        #endregion
    }
}
