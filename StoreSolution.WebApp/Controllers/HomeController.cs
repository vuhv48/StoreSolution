using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StoreSolution.Data.Areas.Stores.ViewModels;
using StoreSolution.WebApp.Helper;
using StoreSolution.WebApp.Models;
using System.Diagnostics;

namespace StoreSolution.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        ApiController _api = new ApiController();
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }


        //hien thi danh sach san pham chi tiet
        public async Task<IActionResult> Index()
        {
            var lastestProducts = new List<ProductVm>();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync("api/ProductApi/lastest-product");
            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                lastestProducts = JsonConvert.DeserializeObject<List<ProductVm>>(result);
            }
            var viewModel = new HomeViewModel
            {
                ProductVms = lastestProducts
            };
            return View(viewModel);
        }


        public async Task<IActionResult> Category()
        {
            var lastestProducts = new List<ProductVm>();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync("api/ProductApi/lastest-product");
            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                lastestProducts = JsonConvert.DeserializeObject<List<ProductVm>>(result);
            }
            var viewModel = new HomeViewModel
            {
                ProductVms = lastestProducts
            };
            return View();
        }

 

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}