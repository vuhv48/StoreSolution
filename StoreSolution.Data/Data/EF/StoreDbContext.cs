
using Microsoft.EntityFrameworkCore;
using StoreSolution.Data.Data.Configs;
using StoreSolution.Data.Models;
using StoreSolution.Data.Areas.Stores.ViewModels;

namespace StoreSolution.Data.Data.EF
{
    public  class StoreDbContext : DbContext
    {
        public StoreDbContext(DbContextOptions options) :base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CategoryConfig());
            modelBuilder.ApplyConfiguration(new ProductConfig());
            modelBuilder.ApplyConfiguration(new ProductInCategoryConfig());
            //base.OnModelCreating(modelBuilder);
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        
        public DbSet<ProductInCategory> ProductCategories { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        //public DbSet<Media> Medias { get; set; }
        //public DbSet<NewsItem> NewsItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        //public DbSet<ProductMedia> ProductMedias { get; set; }
        public DbSet<Shipment> Shipments { get; set; }
        public DbSet<ShipmentItem> ShipmentItems { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Vendor> Vendors { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }

        public DbSet<ProductImage> ProductImages { get; set; }
    }
}
