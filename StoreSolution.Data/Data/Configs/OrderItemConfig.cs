using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StoreSolution.Data.Models;

namespace StoreSolution.Data.Data.Configs
{
    internal class OrderItemConfig : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<OrderItem> builder)
        {
            builder.ToTable("OrderItems");
            builder.HasKey(t => t.Id);
            builder.Property(x => x.OrderID).IsRequired();
            builder.Property(x => x.ProductID).IsRequired();

            builder.HasOne(x => x.Product).WithMany().HasForeignKey(p => p.ProductID).OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Order).WithMany(p => p.OrderItems).HasForeignKey(p => p.OrderID).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
