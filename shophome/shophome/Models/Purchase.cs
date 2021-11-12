using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace ShopHome.Models
{
    public class Purchase
    {
        [DisplayName("Purchase Id")]
        public int PurchaseId { get; set; }
        [DisplayName("Product ID")]
        public int ProductId { get; set; }
        [DisplayName("Product Name")]
        public string ProductName { get; set; }

        public int Price { get; set; }
        public int Quantity { get; set; }
    }
}
