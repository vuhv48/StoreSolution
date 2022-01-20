using Microsoft.EntityFrameworkCore;
using StoreSolution.Data.Areas.Stores.ViewModels;
using StoreSolution.Data.Data.EF;
using StoreSolution.Data.Models;

namespace StoreSolution.Data.Services
{
    public class CartService : ICartService
    {
        private readonly StoreDbContext _context;
        public CartService(StoreDbContext context)
        {
            _context = context;
        }
        public async Task<AddToCartResult> AddToCart(int customerId, int productId, int quantity)
        {
            return await AddToCart(customerId,customerId,productId,quantity);
        }

        public async Task<AddToCartResult> AddToCart(int customerId, int createdById, int productId, int quantity)
        {
            var addToCartResult = new AddToCartResult { Success  = false };
            if(quantity <= 0)
            {
                addToCartResult.ErrorMessage = "So luong phai lon hon 0"; 
                addToCartResult.ErrorCode = "SAI SO LUONG";
                return addToCartResult;
            }
            var cart = await GetActiveCart(customerId, createdById);
            if (cart == null)
            {
                cart = new Cart
                {
                    CustomerId = customerId,
                    CreateById = createdById
                };
                _context.Add(cart);
                await _context.SaveChangesAsync();
            }
            CartItem cartItem = cart.Items.FirstOrDefault(x=>x.ProductId == productId);
            if(cartItem == null)
            {
                cartItem = new CartItem
                {
                    Cart = cart,
                    ProductId = productId,
                    Quantity = quantity,
                    CreateDate = DateTime.Now,
                    CartId = cart.Id,
                   

                };
                _context.CartItems.Add(cartItem);



            }
            else
            {
                cartItem.Quantity = quantity;//cartItem.Quantity + quantity;
                _context.CartItems.Update(cartItem);
            }

            await _context.SaveChangesAsync();
            addToCartResult.Success = true;
            return addToCartResult;
        }

        public  Task<Cart> GetActiveCart(int customerId)
        {
            return  GetActiveCart(customerId,customerId);
        }

        public async Task<Cart> GetActiveCart(int customerId, int createdById)
        {
            var cart = await _context.Carts.Where(x => x.CustomerId == customerId && x.CreateById == createdById && x.IsActive).FirstOrDefaultAsync();
            var query = from c in _context.Carts
                        join ci in _context.CartItems on c.Id equals ci.CartId
                        where (c.CustomerId == customerId && c.CreateById == createdById && c.IsActive)
                        select new { c, ci };
            cart.Items = query.Select(x => new CartItem
            {
                Id = x.ci.Id,
                CreateDate = x.ci.CreateDate,
                ProductId = x.ci.ProductId,
                Quantity = x.ci.Quantity,
                CartId = x.ci.CartId,
            }).ToList();
            return cart;
        }

        public async Task<CartVm> GetActiveCartDetails(int customerId)
        {
            
            return await GetActiveCartDetails(customerId, customerId);
        }

        public async Task<CartVm> GetActiveCartDetails(int customerId, int createdById)
        {
            var cart = await GetActiveCart(customerId, createdById);
            if (cart == null)
            {
                return null;
            }
            var cartVm = new CartVm()
            {
                Id = cart.Id,
                CouponCode = cart.CouponCode,

            };
            var query = from c in _context.Carts  
                        join ci in _context.CartItems on c.Id equals ci.CartId
                        join p in _context.Products on ci.ProductId equals p.Id
                        select new { ci, p,c };
            cartVm.Items = query.Select(x=> new CartItemVm
            {
                Id = x.ci.Id,
                ProductId = x.ci.ProductId,
                Quantity = x.ci.Quantity,
                ProductName = x.p.Name,
                Price = x.p.Price,
                Total = x.p.Price * x.ci.Quantity
            }).ToList();
            cartVm.SubTotal = cartVm.Items.Sum(x => x.Quantity * x.Price);
            return cartVm;
        }

        public Task MigrateCart(int fromUserId, int toUserId)
        {
            throw new NotImplementedException();
        }

        public  IQueryable<Cart> Query()
        {
            return _context.Carts;
        }

        public Task UnlockCart(Cart cart)
        {
            throw new NotImplementedException();
        }

    }
}
