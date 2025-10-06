namespace Services.Models;

public class OrderDetailModel
{
    public Guid OrderId { get; set; }

    public ushort LineNumber { get; set; }

    public uint Quantity { get; set; }

    public OrderModel Order { get; set; } = null!;
    public ProductModel Product { get; set; } = null!;
}
