using Database.Entities;

namespace Services.Models;

public class ProductModel
{
    public string Name { get; set; } = string.Empty; 

    public string? Description { get; set; } 

    public decimal? Price { get; set; } 

}
