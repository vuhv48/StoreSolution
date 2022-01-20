using Microsoft.AspNetCore.Mvc;
using StoreSolution.Data.Areas.Stores.ViewModels;
using StoreSolution.WebApp.Helper;
using System.Net.Http;
using System.Net.Http.Json;

namespace StoreSolution.WebApp.Controllers.Admin
{
    public class BrandController : Controller
    {
        ApiController _api = new ApiController();
        IEnumerable<BrandVm> _brandVms = null;
        public async Task<IActionResult> Index()
        {
            HttpClient client = _api.Initial();
            var responseTask = client.GetAsync("api/BrandApi");
            responseTask.Wait();

            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<List<BrandVm>>();
                readTask.Wait();
                _brandVms = readTask.Result;
            }
            else
            {
                _brandVms = Enumerable.Empty<BrandVm>();
                ModelState.AddModelError(string.Empty, "Server Error");
            }
            return View(_brandVms);
        }
        

        public async Task<IActionResult> Create(BrandVm brandVm)
        {
            HttpClient client = _api.Initial();
            var postTask = client.PostAsJsonAsync<BrandVm>("api/BrandApi", brandVm);
            postTask.Wait();
            var result = postTask.Result;
            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            ModelState.AddModelError(string.Empty, "Server Error");
            return View(_brandVms);
        }

        public async Task<IActionResult> Edit(int id)
        {
            HttpClient client = _api.Initial();
            BrandVm brandVm = null;
            var responseTask = client.GetAsync($"api/BrandApi/{id}");
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<BrandVm>();
                readTask.Wait();
                brandVm = readTask.Result;
            }
            return View(brandVm);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(BrandVm brandVm)
        {
            HttpClient client = _api.Initial();
            var putTask = client.PutAsJsonAsync<BrandVm>("api/BrandApi", brandVm);
            putTask.Wait();
            var result = putTask.Result;
            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View(brandVm);
        }
        public async Task<IActionResult> Delete(int id)
        {
            HttpClient client = _api.Initial();
            var deleteTask = client.DeleteAsync($"api/BrandApi/{id}");
            deleteTask.Wait();
            var result = deleteTask.Result;
            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

    }
}
