namespace StoreSolution.Data.Areas.Stores.ViewModels
{
    public class ProductUpdateRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string? Description { get; set; }


        public decimal Price { get; set; }

        public int CreateById { get; set; }

        public int StockQuantity { get; set; }
        public int? VendorId { get; set; }
        public int? BrandId { get; set; }

        public IFormFile ThumbnailImage { get; set; }
    }
}
