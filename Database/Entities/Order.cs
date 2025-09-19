using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Entities;

//[PrimaryKey(nameof(Id))]
public class Order
{
  //  [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; } // [id]

   // [Column(TypeName = "datetime")]
    public DateTime Date { get; set; } // [date]

    //[ForeignKey(nameof(Manager))]
    //public Guid ManagerId { get; private set; } // [managerId]

    //[ForeignKey(nameof(Customer))]
    //public int? CustomerId { get; private set; } // [customerId]

    // navigation properties
    public virtual Manager Manager { get; set; } = null!;
    public virtual Customer? Customer { get; set; } 
    public virtual List<OrderDetail> OrderDetails { get; set; } = [];

    // custom function to calculate the total sum of the order
    public decimal? GetSum()
    {
       var sum = OrderDetails.Sum(od => od.Quantity * od.Product.Price);

       return sum;
    }
}
