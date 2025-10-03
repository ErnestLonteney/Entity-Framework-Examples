using Microsoft.AspNetCore.Mvc;
using MyShop.Models;
using Services.Interfaces;
using Services.Models;

namespace MyShop.Controllers
{
    public class ProductController(IProductService productService) : Controller
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

            var productsForView = products.Select(p => new ProductViewModel
            {
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
            }).ToList();

            return View(productsForView);
        }
    }
}
