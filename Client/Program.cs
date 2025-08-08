using Database.Entities;

namespace ConsoleApp37
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using var db = new ShopDbContext();

            var customer = new Customer
            {
                Name = "John Doe",
                Email = "john.doe@outlook.com"
            };

            var product1 = new Product
            {
                Name = "Laptop",
                Description = "High performance laptop",
                Price = 1200.00m
            };

            var product2 = new Product
            {
                Name = "Smartphone",
                Description = "Latest model smartphone",
                Price = 800.00m
            };


            var product3 = new Product("Keyboard", 22.45m);

            product3.OrderDetailes.FirstOrDefault();



            db.Products.AddRange(product1, product2, product3);
            db.Customers.Add(customer);


            var order = new Order
            {
                Date = DateTime.Now,
                Customer = customer,
                Manager = new Manager
                {
                    FirstName = "Alice",
                    LastName = "Smith",
                    Email = "smith@rozetka.com.ua"
                }
            };

             var manager11 = db.Managers.Find(order.ManagerId); 

            var orderDetails = new List<OrderDetail>
            {
                new()
                {
                    Order = order,
                    LineNumber = 1,
                    Product = product1,
                    Quantity = 2
                },
                new()
                {
                    Order = order,
                    LineNumber = 2,
                    Product = product2,
                    Quantity = 3
                }
            };

            db.OrderDetailes.AddRange(orderDetails);

            db.SaveChanges();

            Console.WriteLine(order.GetSum());
        }
    }
}
