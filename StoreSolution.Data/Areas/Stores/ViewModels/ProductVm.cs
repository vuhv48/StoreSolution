namespace StoreSolution.Data.Areas.Stores.ViewModels
{
    public class ProductVm
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string? Description { get; set; }


        public decimal Price { get; set; }
        public int StockQuantity { get; set; }

        public int? VendorId { get; set; }
        public int? BrandId { get; set; }

        public string ThumbnailImage { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
