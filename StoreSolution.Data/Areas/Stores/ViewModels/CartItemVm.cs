namespace StoreSolution.Data.Areas.Stores.ViewModels
{
    public class CartItemVm
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }

        public string? ProductName { get; set; }

        public decimal? Price { get; set; }

        public decimal? Total { get; set; }

    }
}
