
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Entities;

public class Customer
{
    [Key] // you are not requited to use this attribute - you can relay on conventions
    public int Id { get; private set; } 

    [MaxLength(200)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(200)]
    public string? Email { get; set; }

   [MaxLength(15)]
   [Column("ContactNumber", TypeName = "varchar(15)")] // varchar instead of nvarchar
    public string? PhoneNumber { get; set; }
    
    public List<Order> Orders { get; set; } = [];
}
