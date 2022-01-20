using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreSolution.Data.Areas.Stores.ViewModels;
using StoreSolution.Data.Data.EF;
using StoreSolution.Data.Models;
using StoreSolution.Data.Services;

namespace StoreSolution.Data.Areas.Stores.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartApiController : ControllerBase
    {
        private readonly StoreDbContext _context;
        private readonly ICartService _cartService;

        public CartApiController(StoreDbContext context, ICartService cartService)
        {
            _cartService = cartService;
            _context = context;
        }

        [HttpGet("customers/{customerId}/cart")]
        public async Task<CartVm> Get(int customerId)
        {
            var cart = await _cartService.GetActiveCartDetails(customerId, customerId);
            return cart;
        }

        [HttpPost("customer/{customerId}/add-cart-item")]
        public async Task<IActionResult> AddToCart(int customerId, [FromBody] AddToCartModel model)
        {
            await _cartService.AddToCart(customerId, customerId, model.ProductId, model.Quantity);
            return Accepted();
        }
        [HttpDelete("/carts/items/{itemId}")]
        public async Task<IActionResult> Delete(int itemId)
        {
            int customerId = 1;
            return null;
        }

       
    }
}
