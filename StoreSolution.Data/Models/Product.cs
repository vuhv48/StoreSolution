using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreSolution.Data.Models
{
    public class Product
    {

        public int Id { get; set; }

        public string Name { get; set; }

        public string? Description { get; set; }


        public decimal Price { get; set; }

        public int CreateById { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;

        public int StockQuantity { get; set; }

        public bool IsDeleted { get; set; }
        public int? VendorId { get; set; }
        public int? BrandId { get; set; }

        public IList<ProductInCategory> ProductInCategories { get; set;}

        public Brand Brand { get; set; }

        public IList<ProductImage> ProductImages { get; set; }


    }
}
