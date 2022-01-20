using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StoreSolution.Data.Areas.Stores.ViewModels;
using StoreSolution.Data.Data.EF;
using StoreSolution.Data.Models;
using StoreSolution.Data.Services;

namespace StoreSolution.Data.Areas.Stores.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandApiController : ControllerBase
    {
        private readonly StoreDbContext _context;
        private readonly IBrandService _brandService;
        public BrandApiController(StoreDbContext context, IBrandService brandService)
        {
            _context = context;
            _brandService = brandService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BrandVm>>> Get()
        {
            IList<Brand> brands = _context.Brands.Where(b => b.IsDeleted.Equals(false)).ToList();
            var brandVms = new List<BrandVm>();
            foreach (var brand in brands)
            {
                var brandVm = new BrandVm
                {
                    Id = brand.Id,
                    Name = brand.Name,
                    Description = brand.Description

                };
                brandVms.Add(brandVm);
            }
            return brandVms.OrderBy(b => b.Id).ToList();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<BrandVm>> Get(int id)
        {
            var brand = _context.Brands.Where(b => b.IsDeleted.Equals(false)).Where(b => b.Id.Equals(id)).FirstOrDefault();
            if (brand == null) { return NotFound(); }
            var brandVm = new BrandVm
            {
                Id =brand.Id,
                Description = brand.Description,
                Name = brand.Name,
            };
            return brandVm;

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var brand = _context.Brands.Where(b => b.IsDeleted.Equals(false)).Where(b => b.Id.Equals(id)).FirstOrDefault();
            if (brand == null) { return NotFound(); }

            await _brandService.Delete(brand);
            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult<BrandVm>> Post([FromBody] BrandVm brandVm)
        {
            if (ModelState.IsValid)
            {
                var brand = new Brand
                {
                    Id=brandVm.Id,
                    Name = brandVm.Name,
                    Description = brandVm.Description
                    
                };
                await _brandService.Create(brand);
                return CreatedAtAction(nameof(Get), new {id= brandVm.Id}, null);

                return Ok();
            }
            return BadRequest(ModelState);
        }
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] BrandVm model)
        {
            if (ModelState.IsValid)
            {
                var brand = _context.Brands.Where(b => b.IsDeleted.Equals(false)).Where(b => b.Id.Equals(model.Id)).FirstOrDefault();
                if(brand == null) { return NotFound(); }
                brand.Name = model.Name;
                brand.Description = model.Description;
                
                await _brandService.Update(brand);
                return Accepted();
            }
            return BadRequest(ModelState);
        }
        
    }
}
