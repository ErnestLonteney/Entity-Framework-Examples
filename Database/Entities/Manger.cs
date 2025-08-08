using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Entities
{
    [Table("SuperUsers")]
    public  class Manager
    {
        [Key]
        public Guid Id { get; set; }

        [StringLength(200)]
        public string FirstName { get; set; } = string.Empty;

        [StringLength(200)]
        public string LastName { get; set; } = string.Empty;

        [StringLength(300)]
        public string? Email { get; set; }

        [StringLength(15)]
        public string? Phone { get; set; }

        public List<Order> Orders { get; set; }
    }
}
