using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StoreSolution.Data.Areas.Stores.ViewModels;
using StoreSolution.WebApp.Helper;
using System.Net.Http.Json;

namespace StoreSolution.WebApp.Controllers
{
    public class CategoryController : Controller
    {
        ApiController _api = new ApiController();
        public async Task<IActionResult> Index()
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

        

        
        public async Task<IActionResult> Delete(int id)
        {
            var categoryVm = new CategoryVm();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.DeleteAsync($"api/categoryApi/{id}");
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            CategoryVm categoryVm = new CategoryVm();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync($"api/CategoryApi/{id}");
            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                categoryVm = JsonConvert.DeserializeObject<CategoryVm>(result);
            }
            return View(categoryVm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CategoryVm categoryVm)
        {
            HttpClient client = _api.Initial();
            var putTask = client.PutAsJsonAsync<CategoryVm>("api/CategoryApi/", categoryVm);
            putTask.Wait();
            var result = putTask.Result;
            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View(categoryVm);
        }

        public async Task<IActionResult> Create([Bind("Name, Description")]CategoryVm categoryVm)
        {
            HttpClient client = _api.Initial();
            var postTask = client.PostAsJsonAsync<CategoryVm>("api/CategoryApi/", categoryVm);
            postTask.Wait();
            var result = postTask.Result;
            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            ModelState.AddModelError(string.Empty, "Server Error.");
            return View(categoryVm);
        }

        
        
    }
}
