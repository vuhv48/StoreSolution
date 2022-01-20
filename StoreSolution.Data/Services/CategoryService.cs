using StoreSolution.Data.Areas.Stores.ViewModels;
using StoreSolution.Data.Data.EF;
using StoreSolution.Data.Models;

namespace StoreSolution.Data.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly StoreDbContext _context;
        public CategoryService(StoreDbContext context)
        {
            _context = context;
        }
       

        public async Task<List<CategoryVm>> GetAll()
        {
            IList<Category> categories = _context.Categories.Where(c => c.IsDeleted.Equals(false)).ToList();
            var categoriesList = new List<CategoryVm>();
            foreach (var category in categories)
            {
                var categoryListItem = new CategoryVm
                {
                    Id = category.Id,
                    Name = category.Name,
                    ParentId = category.ParentId

                };
                //var parentCategory = category.Parent;
                //while(parentCategory != null)
                //{
                //    category.Name = $"{parentCategory.Name} >> {categoryListItem.Name}";
                //    parentCategory = parentCategory.Parent;
                //}
                categoriesList.Add(categoryListItem);
            }
            return categoriesList.OrderBy(x => x.Name).ToList();
        }

        public async Task Update(Category category)
        {
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
        }

        public async Task Create(Category category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Category category)
        {
            category.IsDeleted = true;
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
        }
    }
}
