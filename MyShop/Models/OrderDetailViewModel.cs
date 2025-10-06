namespace MyShop.Models;

public class OrderDetailViewModel
{
    public Guid OrderId { get; set; }

    public ushort LineNumber { get; set; }

    public uint Quantity { get; set; }

    public OrderViewModel Order { get; set; } = null!;
    public ProductViewModel Product { get; set; } = null!;
}
