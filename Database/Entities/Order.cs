
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Entities
{
    public class Order
    {
        [Key]
        public int Id { get; set; } // id

        [Column(TypeName = "datetime")]
        public DateTime Date { get; set; } // date


        public Guid ManagerId { get; set; } // managerId

        public int? CustomerId { get; set; } // customerId

        public Customer? Customer { get; set; } // customerId

        public Manager Manager { get; set; } = null!; // managerId

        public List<OrderDetail> OrderDetailes { get; set; } = [];


        public decimal? GetSum()
        {
           var sum = OrderDetailes.Sum(od => od.Quantity * od.Product.Price);

           return sum;
        }
    }
}
