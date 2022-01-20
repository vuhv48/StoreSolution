using StoreSolution.Data.Areas.Stores.ViewModels;
using StoreSolution.Data.Models;
namespace StoreSolution.Data.Services
{
    public interface ICartService
    {
        Task<AddToCartResult> AddToCart(int customerId, int productId, int quantity);

        Task<AddToCartResult> AddToCart(int customerId, int createdById, int productId, int quantity);

        IQueryable<Cart> Query();
        Task<Cart> GetActiveCart(int customerId);

        Task<Cart> GetActiveCart(int customerId, int createdById);

        Task<CartVm> GetActiveCartDetails(int customerId);

        Task<CartVm> GetActiveCartDetails(int customerId, int createdById);

        //Task<CouponValidationResult> ApplyCoupon(long cartId, string couponCode);

        Task MigrateCart(int fromUserId, int toUserId);

        Task UnlockCart(Cart cart);


    }
}
