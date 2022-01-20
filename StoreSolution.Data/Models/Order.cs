using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreSolution.Data.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int OrderStatus { get; set; }
        public string CouponCode { get; set; }
        public string PaymentMethod { get; set; }
        public int ParentId { get; set; }
        public string ShippingMethod { get; set; }
        public decimal OrderTotal { get; set; }

        public IList<OrderItem> OrderItems { get; set; }
    }
}
