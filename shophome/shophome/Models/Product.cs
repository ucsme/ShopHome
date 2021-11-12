using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopHome.Models
{
    public class Product
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public int Price { get; set; }
        public int QuantityAvaliable { get; set; }
    }
}