using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StoreSolution.Data.Models;

namespace SStoreSolution.Data.Data.Configs
{
    public class ShipmentItemConfig : IEntityTypeConfiguration<ShipmentItem>
    {
        public void Configure(EntityTypeBuilder<ShipmentItem> builder)
        {
            builder.ToTable("ShipmentItems");
            builder.HasKey(x => x.Id);
            builder.Property(x=>x.ShipmentId).IsRequired();
            builder.Property(x => x.OrderItemId).IsRequired();
            builder.Property(x => x.ProductId).IsRequired();

            builder.HasOne(x=>x.Product).WithMany().HasForeignKey(x => x.ProductId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.Shipment).WithMany(si => si.ShipmentItems).HasForeignKey(si => si.ShipmentId).OnDelete(DeleteBehavior.Restrict);

        }
    }
}
