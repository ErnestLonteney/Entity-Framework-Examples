using Database.Entities;

namespace Client
{
    internal class Program
    {
        static void Main()
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
            { // using constructor with parameters
                new() { Order = order, Product = product1, LineNumber = 1, Quantity = 2 },
                new() { Order = order, Product = product2, LineNumber = 2, Quantity = 50 }             
            };

            db.OrderDetailes.AddRange(orderDetails);


            var dep1 = new Department
            {
                Name = "It"
            };

            var dep2 = new Department
            {
                Name = "QA"
            };

            var man1 = new Manager
            {
                FirstName = "Ben",
                LastName = "Aflec",
            };

            var man2 = new Manager
            {
                FirstName = "Olha",
                LastName = "Dovgan",
            };



            dep1.Managers.AddRange(man1, man2);
            man2.Departments.Add(dep2);

            db.Departments.AddRange(dep1, dep2);

            db.SaveChanges();

            Console.WriteLine(order.GetSum());
        }
    }
}
