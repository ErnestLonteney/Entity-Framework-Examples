using Microsoft.AspNetCore.Mvc;

namespace MyShop.Controllers
{
    public class OrderController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
