using Services.Models;

namespace Services.Interfaces;

public interface IOrderService
{
    List<OrderModel> GetAllOrdersInPeriod(DateTime from, DateTime to);
}
