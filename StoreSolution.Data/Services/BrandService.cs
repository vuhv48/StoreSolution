using StoreSolution.Data.Areas.Stores.ViewModels;
using StoreSolution.Data.Data.EF;
using StoreSolution.Data.Models;

namespace StoreSolution.Data.Services
{
    public class BrandService : IBrandService
    {
        private readonly StoreDbContext _context;
        public BrandService(StoreDbContext context)
        {
            _context = context;
        }
        public async Task Create(Brand brand)
        {
            _context.Brands.Add(brand);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Brand brand)
        {
            brand.IsDeleted = true;
            _context.Brands.Update(brand);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<BrandVm>> GetAll()
        {
            IList<Brand> brands =  _context.Brands.Where(b => b.IsDeleted.Equals(false)).ToList();
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

      

        public async Task Update(Brand brand)
        {
            _context.Brands.Update(brand);
            await _context.SaveChangesAsync();
        }
    }
}
