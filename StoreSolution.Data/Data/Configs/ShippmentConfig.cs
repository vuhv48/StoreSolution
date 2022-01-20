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
    internal class ShippmentConfig : IEntityTypeConfiguration<Shipment>
    {
        public void Configure(EntityTypeBuilder<Shipment> builder)
        {
            builder.ToTable("Shipments");
            builder.HasKey(x => x.Id);
            builder.Property(x=>x.OrderId).IsRequired();
            builder.Property(x=>x.CreateById).IsRequired();

            builder.HasOne(x=>x.Warehouse).WithMany().HasForeignKey(x=>x.WarehouseId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x=>x.Order).WithMany().HasForeignKey(s=>s.OrderId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
