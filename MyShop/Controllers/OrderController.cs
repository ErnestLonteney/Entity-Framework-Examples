using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace MyShop.Controllers
{
    public class OrderController(IOrderService orderService, IMapper mapper) : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        // GET: Order/OrdersInPeriod?from=2023-01-01&to=2023-12-31
        [HttpGet]
        public IActionResult GetOrdersInPeriod(DateTime from, DateTime to)
        {
            var results = orderService.GetAllOrdersInPeriod(from, to);  

            if (results == null || !results.Any())
            {
                return NotFound("No orders found in the specified period.");
            }   

            var ordersForView = mapper.Map<List<Models.OrderViewModel>>(results);   

            return View(ordersForView);
        }
    }
}
