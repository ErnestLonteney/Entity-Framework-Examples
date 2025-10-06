using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MyShop.Models;
using Services.Interfaces;
using Services.Models;

namespace MyShop.Controllers
{
    public class ProductController(IProductService productService, IMapper mapper) : Controller
    {
        public IActionResult Index()
        {
            List<ProductModel> products;

            try
            {
                products = productService.GetAllProducts();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }   

            if (products == null)
            {
                return NotFound();
            }

            var productsForView = mapper.Map<List<ProductViewModel>>(products); 

            return View(productsForView);
        }
    }
}
