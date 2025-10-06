namespace MyShop.Models;

public class OrderViewModel
{
    public DateTime Date { get; set; }

    public List<OrderDetailViewModel> OrderDetails { get; set; } = [];
}
