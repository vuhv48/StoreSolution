using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreSolution.Data.Models
{
    public  class Category
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int? ParentId { get; set; }

        public bool IsDeleted { get; set; }
        public IList<ProductInCategory> ProductInCategories { get; set; }

        public Category Parent { get; set; }
    }
}
