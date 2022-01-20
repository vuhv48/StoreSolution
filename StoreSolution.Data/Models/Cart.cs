using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreSolution.Data.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int CreateById { get; set; }
        public bool IsActive { get; set; }
        
        public string? OrderNote { get; set; }
        public DateTime OrderDate { get; set; }
        public string? CouponCode { get; set; }

        public IList<CartItem> Items { get; set; } = new List<CartItem>();
    }
}

