using AutoMapper;
using Database.Entities;
using Microsoft.EntityFrameworkCore;
using Services.Interfaces;
using Services.Models;

namespace Services
{
    public class OrderService(ShopDbContext shopDbContext, IMapper mapper) :  IOrderService
    {
        public List<OrderModel> GetAllOrdersInPeriod(DateTime from, DateTime to)
        {
            List<Order> ordersInDbModels = shopDbContext.Orders
                .Include(o => o.OrderDetails)
                .ThenInclude(od => od.Product)
                .Where(o => o.Date >= from && o.Date <= to)
                .ToList();

            return mapper.Map<List<OrderModel>>(ordersInDbModels);  
        }
    }
}
