using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StoreSolution.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreSolution.Data.Data.Configs
{
    internal class ProductConfig : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x=>x.Price).IsRequired();
            builder.Property(x => x.IsDeleted).IsRequired();
            builder.Property(x => x.StockQuantity).HasDefaultValue(0);

            builder.HasOne(x => x.Brand).WithMany(b => b.Products).OnDelete(DeleteBehavior.Restrict);

        }
    }
}
