using StoreSolution.Data.Areas.Stores.ViewModels;
using StoreSolution.Data.Models;

namespace StoreSolution.Data.Services
{
    public interface ICategoryService
    {
        Task<List<CategoryVm>> GetAll();

        Task Create(Category category);

        Task Update(Category category);

        Task Delete(Category category);

    }
}
