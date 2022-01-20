using Microsoft.AspNetCore.Http;
namespace StoreSolution.Data.Areas.Stores.ViewModels
{
    public class ProductCreateRequest
    {
        public string Name { get; set; }

        public string? Description { get; set; }


        public decimal Price { get; set; }

        public int CreateById { get; set; }

        public int StockQuantity { get; set; }
        public int? VendorId { get; set; }
        public int? BrandId { get; set; }
        
        public int CategoryId { get; }
        public IFormFile ThumbnailImage { get; set; }
    }
}
