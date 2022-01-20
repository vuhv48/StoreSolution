using Microsoft.AspNetCore.Mvc;
using StoreSolution.Data.Areas.Stores.ViewModels;
using StoreSolution.Data.Data.EF;
using StoreSolution.Data.Models;
using StoreSolution.Data.Services;

namespace StoreSolution.Data.Areas.Stores.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryApiController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly StoreDbContext _context;
        public CategoryApiController(ICategoryService categoryService, StoreDbContext context)
        {
            _categoryService = categoryService;
            _context = context;
        }

        [HttpGet]
        public async Task<List<CategoryVm>> Get()
        {
            return await _categoryService.GetAll();
        }
       
        [HttpGet("{id}")]
        public CategoryVm Get(int id)
        {
           var category = _context.Categories.Where(x=>x.Id == id).SingleOrDefault();
           var categoryVm = new CategoryVm
           {
                Id = category.Id,
                Description = category.Description,
                Name = category.Name,
                ParentId = category.ParentId
           };
            return categoryVm;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var category =  _context.Categories.FirstOrDefault(c => c.Id == id);
            if(category == null)
            {
                return NotFound();
            }
            await _categoryService.Delete(category);
            return NoContent();
        }

        //[HttpPut("{id}")]
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] CategoryVm model)
        {
            if (ModelState.IsValid)
            {
                var category = _context.Categories.FirstOrDefault(c => c.Id == model.Id);
                if(category == null)
                {
                    return NotFound();
                }
                category.Name = model.Name;
                category.Description = model.Description;
                category.ParentId = model.ParentId;

                await _categoryService.Update(category);
                return Accepted();
            }
            return BadRequest(ModelState);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CategoryVm model)
        {
            if (ModelState.IsValid)
            {
                var category = new Category
                {
                    Name = model.Name,
                    Description = model.Description,
                    ParentId = model.ParentId

                };
                await _categoryService.Create(category);
                return CreatedAtAction(nameof(Get), new {id = category.Id}, null);
            }
            return BadRequest(ModelState);
        }

        
    }
}
