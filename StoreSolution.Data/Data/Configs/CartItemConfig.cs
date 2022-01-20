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
    public class CartItemConfig : IEntityTypeConfiguration<CartItem>
    {
        public void Configure(EntityTypeBuilder<CartItem> builder)
        {
            builder.ToTable("CartItems");
            builder.HasKey(x => x.Id);
            builder.Property(x=>x.ProductId).IsRequired();
            builder.Property(x=>x.CartId).IsRequired();
            builder.Property(x=>x.Quantity).HasDefaultValue(0);

            builder.HasOne(x=>x.Product).WithMany().HasForeignKey(x=>x.ProductId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x=>x.Cart).WithMany(c=>c.Items).HasForeignKey(x=>x.CartId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
