using Microsoft.EntityFrameworkCore;

namespace Database.Entities;

// [Index(nameof(FirstName), nameof(LastName), IsUnique = false)]
public class Person
{
   // [Key]
    public Guid Id { get; private set; }

  //  [MaxLength(200)]
    public string FirstName { get; set; } = string.Empty;

 //   [MaxLength(200)]
    public string LastName { get; set; } = string.Empty;

  //  [MaxLength(300)]
    public string? Email { get; set; }

   // [MaxLength(15)]
    public string? Phone { get; set; }

    public virtual Address? Address { get; set; }

    public DateOnly DateOfBirth { get; set; }
}
