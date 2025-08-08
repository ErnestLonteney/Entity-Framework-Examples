using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Entities;

[PrimaryKey(nameof(OrderId), nameof(LineNumber))]
public class OrderDetail
{
    [ForeignKey(nameof(Order))]
    public int OrderId { get; set; }

    public ushort LineNumber { get; set; }

    public Product Product { get; set; } = null!;

    public uint Quantity { get; set; }

    public Order Order { get; set; } = null!;

}
