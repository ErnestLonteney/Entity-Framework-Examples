using AutoMapper;
using Database.Entities;
using Services.Interfaces;
using Services.Models;

namespace Services
{
    public class ProductService(ShopDbContext context, IMapper mapper) : IProductService
    {
        public List<ProductModel> GetAllProducts()
        {
            List<Product> products = context.Products.ToList();

            if (products.Count() > 100)
            {
                products = products.Take(100).ToList();
            }

           var mappedProducts = mapper.Map<List<ProductModel>>(products);

            return mappedProducts;
        }
    }
}
