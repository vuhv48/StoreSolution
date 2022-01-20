using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using StoreSolution.Data.Areas.Stores.ViewModels;
using StoreSolution.WebApp.Helper;
using System.Net.Http;
using System.Net.Http.Json;

namespace StoreSolution.WebApp.Controllers.Admin
{
    public class ProductController : Controller
    {
        ApiController _api = new ApiController();
        public async Task<IActionResult> Index()
        {
            List<ProductVm> productVms = new List<ProductVm>();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync("api/ProductApi");
            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                productVms = JsonConvert.DeserializeObject<List<ProductVm>>(result);
            }

            var categoryRes = await client.GetAsync("api/CategoryApi");
            var categories = new List<CategoryVm>();
            if (categoryRes.IsSuccessStatusCode)
            {
                var resultCategories = categoryRes.Content.ReadAsStringAsync().Result;
                categories = JsonConvert.DeserializeObject<List<CategoryVm>>(resultCategories);
            }
            ViewBag.Categories = categories.Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id.ToString()
            });

            return View(productVms);
        }

        public async Task<IActionResult> Edit(int productId)
        {
            return null;
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProductUpdateRequest request)
        { 
            HttpClient client = _api.Initial();
            var postTask = client.PutAsJsonAsync("api/ProductApi", request);
            postTask.Wait();
            var result = postTask.Result;
            if (result.IsSuccessStatusCode) { 
                return RedirectToAction("Index");
            }
            return View(request);

        }
        
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] ProductCreateRequest request)
        {
            HttpClient client = _api.Initial();
            var requestContent = new MultipartFormDataContent();
            if (request.ThumbnailImage != null)
            {
                byte[] data;
                using (var br = new BinaryReader(request.ThumbnailImage.OpenReadStream()))
                {
                    data = br.ReadBytes((int)request.ThumbnailImage.OpenReadStream().Length);
                }
                ByteArrayContent bytes = new ByteArrayContent(data);
                requestContent.Add(bytes, "ThumbnailImage", request.ThumbnailImage.FileName);
            }
            requestContent.Add(new StringContent(request.Name.ToString()), "Name");
            requestContent.Add(new StringContent(request.Name.ToString()), "Description");
            requestContent.Add(new StringContent(request.Name.ToString()), "Price");
            requestContent.Add(new StringContent(request.Name.ToString()), "CreateById");
            requestContent.Add(new StringContent(request.Name.ToString()), "StockQuantity");
            requestContent.Add(new StringContent(request.Name.ToString()), "VendorId");
            requestContent.Add(new StringContent(request.Name.ToString()), "BrandId");
            
            var postTask = client.PostAsync($"api/ProductApi", requestContent);
            var result = postTask.Result;
            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            ModelState.AddModelError(string.Empty, "Server Error.");
            return View(requestContent);
        }

        public IActionResult Detail(int id)
        {

            HttpClient client = _api.Initial();
            ProductVm productVm = null;
            var responseTask = client.GetAsync($"api/ProductApi/{id}");
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<ProductVm>();
                readTask.Wait();
                productVm = readTask.Result;
            }
            return View(productVm);
        
        }

        
        public IActionResult ProductWithCategory(int id)
        {
            List<ProductVm> productVms = new List<ProductVm>();
            HttpClient client = _api.Initial();
            var res = client.GetAsync($"api/ProductApi/product-with-category/{id}");
            res.Wait();
            var result = res.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsStringAsync().Result;
                productVms = JsonConvert.DeserializeObject<List<ProductVm>>(readTask);
            }

            return View(productVms);
        }


    }
}
