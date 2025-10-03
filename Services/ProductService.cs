using Database.Entities;
using Services.Interfaces;
using Services.Models;

namespace Services
{
    public class ProductService(ShopDbContext context) : IProductService
    {
        public List<ProductModel> GetAllProducts()
        {
            var products = context.Products.Select(product => new ProductModel
            {
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
            }).ToList();

            if (products.Count > 100)
            {
                products = products.Take(100).ToList();
            }

            return products;
        }
    }
}
