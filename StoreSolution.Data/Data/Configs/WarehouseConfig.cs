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
    public class WarehouseConfig : IEntityTypeConfiguration<Warehouse>
    {
        public void Configure(EntityTypeBuilder<Warehouse> builder)
        {
            builder.ToTable("Warehouses");
            builder.HasKey(x => x.Id);
            builder.Property(x=>x.VendorId).IsRequired();

            builder.HasOne(x => x.Vendor).WithMany().HasForeignKey(w => w.VendorId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
