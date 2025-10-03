using Database.Entities;
using Services.Models;

namespace Services.Interfaces
{
    public interface IProductService
    {
        List<ProductModel> GetAllProducts();     
    }
}
