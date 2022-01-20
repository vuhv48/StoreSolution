using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreSolution.Data.Models 
{
    public class CartItem
    {
        public int Id { get; set; }
        public DateTime CreateDate { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public int CartId { get; set; }

        public Product Product { get; set; }

        public Cart Cart { get; set; }
    }
}
