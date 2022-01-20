using Microsoft.AspNetCore.Mvc;
using StoreSolution.Data.Areas.Stores.ViewModels;
using StoreSolution.WebApp.Helper;

namespace StoreSolution.WebApp.Controllers.User
{
    public class CartController : Controller
    {
        ApiController _api = new ApiController();
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Basket(int customerId)
        {
            customerId = 1;
            HttpClient client = _api.Initial();
            var responseTask = client.GetAsync($"api/CartApi/customers/{customerId}/cart");
            responseTask.Wait();
            CartVm cartVm = new CartVm();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<CartVm>();
                readTask.Wait();
                cartVm = readTask.Result;
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Server Error");
            }
            return View(cartVm);
        }


        [HttpPost]
        public IActionResult UpdateCart(int customer, AddToCartModel model)
        {
            int customerId = 1;;
            HttpClient client = _api.Initial();
            var postTask = client.PostAsJsonAsync($"api/CartApi/customer/{customerId}/add-cart-item",model);
            postTask.Wait();
            var result = postTask.Result;
            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            ModelState.AddModelError(string.Empty, "Server Error");
            return null;
        }
         
        public IActionResult Delete(int itemId)
        {
            HttpClient client = _api.Initial();
            var deleteTask = client.DeleteAsync($"/carts/items/{itemId}");
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
