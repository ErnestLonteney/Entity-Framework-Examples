
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Entities
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }

        [StringLength(200)]
        public string Name { get; set; } = string.Empty;

        [StringLength(200)]
        public string? Email { get; set; }

        [StringLength(15)]
        [Column("ContactNumber")]
        public string? PhoneNumber { get; set; }
        
        public List<Order> Orders { get; set; } = new List<Order>();
    }
}
