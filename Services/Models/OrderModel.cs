namespace Services.Models;

public class OrderModel
{
    public Guid Id { get; set; } 
    public DateTime Date { get; set; } 

    public List<OrderDetailModel> OrderDetails { get; set; } = [];
}
