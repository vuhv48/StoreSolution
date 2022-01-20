using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StoreSolution.Data.Models;

namespace StoreSolution.Data.Data.Configs
{
    public class StockConfig : IEntityTypeConfiguration<Stock>
    {
        public void Configure(EntityTypeBuilder<Stock> builder)
        {
            builder.ToTable("Stocks");
            builder.HasKey(x => x.Id);
            builder.Property(x=>x.WarehouseId).IsRequired();
            builder.Property(x=>x.ProductId).IsRequired();
            builder.Property(x => x.Quantity).HasDefaultValue(0);

            builder.HasOne(x=>x.Product).WithMany().HasForeignKey(p=>p.ProductId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x=>x.Warehouse).WithMany().HasForeignKey(w=>w.WarehouseId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
