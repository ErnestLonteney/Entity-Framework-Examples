using AutoMapper;
using Database.Entities;
using Services.Models;

namespace Services.Configuration
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<Product, ProductModel>();
            CreateMap<Order, OrderModel>();
            CreateMap<OrderDetail, OrderDetailModel>();

        }
    }
}
