
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Database.Entities;

[Index(nameof(Name), IsUnique = true)]
[PrimaryKey(nameof(Id))]
public class Product
{
    public Product(string name, decimal price)
    {
        Name = name;
        Price = price;
    }

    public Product()
    {      
    }

    [Key]
    public int Id { get; private set;  } // [id]

    [MaxLength(200)]
    public string Name { get; set; } = string.Empty; // [name]

    public string? Description { get; set; } // [description]
   
    public decimal? Price { get; set; } // [price]

	public decimal RawPrice { get; set; } // [rawPrice]

	// Navigation property for the order details
	public List<OrderDetail> OrderDetailes { get; set; } = []; 
}
