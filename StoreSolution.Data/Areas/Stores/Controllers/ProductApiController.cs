using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreSolution.Data.Areas.Stores.ViewModels;
using StoreSolution.Data.Services;

namespace StoreSolution.Data.Areas.Stores.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductApiController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductApiController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _productService.GetAll();
            if (products == null)
            {
                return BadRequest();
            }
            return Ok(products);
        }

        [HttpGet("{productId}")]
        public async Task<IActionResult> GetById(int productId)
        {
            var product = await _productService.GetById(productId);
            if (product == null)
            {
                return BadRequest();
            }
            return Ok(product);
        }
        [HttpGet("lastest-product")]
        public async Task<IActionResult> GetLatestProduct()
        {
            var products = await _productService.GetLatestProducts();
            if (products == null)
            {
                return BadRequest();
            }
            return Ok(products);
        }
        [HttpGet("product-with-category/{categoryId}")]
        public async Task<IActionResult> GetProductsWithCategory(int categoryId)
        {
            var products = await _productService.GetProductsWithCategory(categoryId);
            if (products == null)
            {
                return BadRequest();
            }
            return Ok(products);
        }


        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Create([FromForm] ProductCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var productId = await _productService.Create(request);
            if (productId == null)
            {
                return BadRequest(ModelState);
            }
            var product = await _productService.GetById(productId);
            return CreatedAtAction(nameof(Create), new { id = productId }, product);

        }

        [HttpPut]
        public async Task<IActionResult> Update([FromForm] ProductUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var afftectedResult = await _productService.Update(request);
            if(afftectedResult == 0)
            {
                return BadRequest();
            }
            return Ok();
        }

        [HttpDelete("{productId}")]
        public async Task<IActionResult> Delete(int productId)
        {
            var affectedResult = await _productService.Delete(productId);
            if (affectedResult == 0)
            {
                return BadRequest();
            }
            return Ok();
        }

        
    }
}
