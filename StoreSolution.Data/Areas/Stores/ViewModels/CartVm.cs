namespace StoreSolution.Data.Areas.Stores.ViewModels
{
    public class CartVm
    {
        public int Id { get; set; }
        public bool IsActive { get; set; }
        public string CouponCode { get; set; }

        public decimal? SubTotal { get; set; }
        public IList<CartItemVm> Items { get; set; }
    }
}
