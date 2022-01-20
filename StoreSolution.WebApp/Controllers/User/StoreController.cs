using Microsoft.AspNetCore.Mvc;

namespace StoreSolution.WebApp.Controllers.User
{
    public class StoreController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
