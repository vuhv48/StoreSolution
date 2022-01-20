using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StoreSolution.Data.Models;


namespace StoreSolution.Data.Data.Configs
{
    public class ProductInCategoryConfig : IEntityTypeConfiguration<ProductInCategory>
    {
        public void Configure(EntityTypeBuilder<ProductInCategory> builder)
        {
            builder.ToTable("ProductInCategories");
            builder.HasKey(x => new {x.CategoryId, x.ProductId});

            builder.HasOne(x => x.Product).WithMany(pc => pc.ProductInCategories)
                .HasForeignKey(pc => pc.ProductId).OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Category).WithMany(pc => pc.ProductInCategories)
                .HasForeignKey(pc => pc.CategoryId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
