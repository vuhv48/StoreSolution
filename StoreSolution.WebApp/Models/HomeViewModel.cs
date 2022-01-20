using StoreSolution.Data.Areas.Stores.ViewModels;

namespace StoreSolution.WebApp.Models
{
    public class HomeViewModel
    {
        public List<ProductVm> ProductVms { get; set; }

        public List<CategoryVm> CategoryVms { get; set; }
        public List <BrandVm> BrandVms { get; set;}
    }
}
