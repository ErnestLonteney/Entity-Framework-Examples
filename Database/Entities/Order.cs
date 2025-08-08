
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Entities;

[PrimaryKey(nameof(Id))]
public class Order
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; private set; } // [id]

    [Column(TypeName = "datetime")]
    public DateTime Date { get; set; } // [date]

    [ForeignKey(nameof(Manager))]
    public Guid ManagerId { get; private set; } // [managerId]

    // navigation property
    public Manager Manager { get; set; } = null!; 

    [ForeignKey(nameof(Customer))]
    public int? CustomerId { get; private set; } // [customerId]

    // navigation property
    public Customer? Customer { get; set; } 

    // navigation property
    public List<OrderDetail> OrderDetailes { get; set; } = [];

    // custom function to calculate the total sum of the order
    public decimal? GetSum()
    {
       var sum = OrderDetailes.Sum(od => od.Quantity * od.Product.Price);

       return sum;
    }
}
