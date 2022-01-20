using StoreSolution.Data.Areas.Stores.ViewModels;

namespace StoreSolution.Data.Services
{
    public interface IProductService
    {
        Task<int> Create(ProductCreateRequest request);
        Task<int> Update(ProductUpdateRequest request);

        Task<int> Delete(int productId);

        Task<ProductVm> GetById(int productId);

        Task<IEnumerable<ProductVm>> GetAll();

        Task<IEnumerable<ProductVm>> GetLatestProducts();

        Task<IEnumerable<ProductVm>> GetProductsWithCategory(int categoryId);

        Task<IEnumerable<ProductVm>> GetProductsWithCategoryAndBrand();



    }
}
