using StoreSolution.Data.Areas.Stores.ViewModels;
using StoreSolution.Data.Models;

namespace StoreSolution.Data.Services
{
    public interface IBrandService
    {
        Task<IEnumerable<BrandVm>> GetAll();
        Task Create(Brand brand);
        Task Delete(Brand brand);
        Task Update(Brand brand);

    }
}
