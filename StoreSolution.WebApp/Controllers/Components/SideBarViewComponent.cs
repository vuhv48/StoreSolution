using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StoreSolution.Data.Areas.Stores.ViewModels;
using StoreSolution.WebApp.Helper;

namespace StoreSolution.WebApp.Controllers.Components
{
    public class SideBarViewComponent  : ViewComponent
    {
        ApiController _api = new ApiController();
        public async Task<IViewComponentResult> InvokeAsync(int maxPriority, bool isDone)
        {
            List<CategoryVm> categoryListItemVms = new List<CategoryVm>();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync("api/CategoryApi");
            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                categoryListItemVms = JsonConvert.DeserializeObject<List<CategoryVm>>(result);
            }
            return View(categoryListItemVms);
        }
    }
}
