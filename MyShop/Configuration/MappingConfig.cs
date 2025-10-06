using AutoMapper;
using MyShop.Models;
using Services.Models;

namespace MyShop.Configuration
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<ProductModel, ProductViewModel>();
            CreateMap<ProductModel, ProductViewModel>();
            CreateMap<OrderModel, OrderViewModel>();
            CreateMap<OrderDetailModel, OrderDetailViewModel>();    
        }
    }
}
