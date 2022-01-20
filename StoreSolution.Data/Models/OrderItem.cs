using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreSolution.Data.Models
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int OrderID { get; set;}

        public int ProductID { get; set;}

        public decimal ProductPrice { get; set;}
        public int Quantity { get; set;}

        public decimal DiscountAmount { get; set;}
        public decimal TaxAmount { get; set;}
        public decimal TaxPercent { get; set;}

        public Order Order { get; set;}
        public Product Product { get; set;}

    }
}
