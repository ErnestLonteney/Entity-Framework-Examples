using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Entities;

[PrimaryKey(nameof(OrderId), nameof(LineNumber))]
public class OrderDetail
{
    [ForeignKey(nameof(Order))]
    public Guid OrderId { get; private set; }

    public ushort LineNumber { get; set; }

    [ForeignKey(nameof(Product))]
    public int ProductId { get; private set; }

    public uint Quantity { get; set; } 

    // NAVIGATION PROPERTIES 
    public Order Order { get; set; } = null!;

    public Product Product { get; set; } = null!;

}
