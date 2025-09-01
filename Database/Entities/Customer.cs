namespace Database.Entities;

public class Customer : Person
{
    public float Discount { get; set; }
    public virtual List<Order> Orders { get; set; } = [];
}
