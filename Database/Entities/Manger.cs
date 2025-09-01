namespace Database.Entities;


// [Table("SuperUsers")]
public  class Manager : Person
{
    public double Salary { get; set; }

    public virtual List<Order> Orders { get; set; } = [];
    public virtual List<Department> Departments { get; set; } = [];
}
