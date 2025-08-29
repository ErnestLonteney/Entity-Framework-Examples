using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Entities;

[Index(nameof(FirstName), nameof(LastName), IsUnique = false)]
[Table("SuperUsers")]
public  class Manager
{
    [Key]
    public Guid Id { get; private set; }

    [MaxLength(200)]
    public string FirstName { get; set; } = string.Empty;

    [MaxLength(200)]
    public string LastName { get; set; } = string.Empty;

    [MaxLength(300)]
    public string? Email { get; set; }

    [MaxLength(15)]
    public string? Phone { get; set; }

    public virtual Address? Address { get; set; }

    public virtual List<Order> Orders { get; set; } = [];

    public virtual List<Department> Departments { get; set; } = [];
}
