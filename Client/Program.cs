using Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Client
{
    internal class Program
    {
        static void Main()
        {
            using var db = new ShopDbContext();

            #region Adding data

            #region Customers

            var customer = new Customer
            {
                FirstName = "John Doe",
                Email = "john.doe@outlook.com"
            };

            db.Customers.Add(customer);

            #endregion

            #region Products
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
            db.Products.AddRange(product1, product2, product3);

            #endregion

            #region Orders and OrderDetails

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

            var manager11 = db.Managers.Find(order.Manager.Id);

            var orderDetails = new List<OrderDetail>
            { // using constructor with parameters
                new() { Order = order, Product = product1, LineNumber = 1, Quantity = 2 },
                new() { Order = order, Product = product2, LineNumber = 2, Quantity = 50 }
            };

            db.OrderDetails.AddRange(orderDetails);

            #endregion

            #region Departments with Managers

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

            var personX = new Manager
            {
                FirstName = "Joe",
                LastName = "Travison",
                Email = "mail@mail.com"
            };

            db.Managers.Add(personX);

            db.SaveChanges();

            #endregion

            #endregion


            #region Queries examples

            var man11 = db.Managers.Single(m => m.FirstName == "Joe" && m.LastName == "Travison");

            man11.Address = new Address
            {
                City = "Kyiv",
                Country = "Ukraine"
            };

            var person = new Person
            {
                FirstName = "Volodymyr",
                LastName = "Gavryluk",
                Address = new Address
                {
                    City = "Kyiv",
                    Country = "Ukraine"
                }
            };

            db.SaveChanges();


            // Eager Loading
            var ordersWithCustomer = db.Orders
                .Include(o => o.OrderDetails) 
                .ThenInclude(od => od.Product)
                .Include(o => o.Customer)
                .Where(o => o.Customer != null);

            Console.WriteLine(ordersWithCustomer.ToQueryString());

            foreach (Order o in ordersWithCustomer)
            {
                Console.WriteLine(o.Date);       
                Console.WriteLine(o.Customer.FirstName); 
                Console.WriteLine(o.GetSum());

                foreach (OrderDetail od in o.OrderDetails) 
                {
                    Console.WriteLine(od.Product.Name);
                }
            }

            var ordedFirst = db.Orders.First();

            // Explixit loading
            db.Entry(ordedFirst)
                .Collection(o => o.OrderDetails)
                .Query()
                .Where(od => od.Quantity > 10)
                .Load();

            db.Entry(ordedFirst)
               .Reference(o => o.Customer)
               .Load();

            Console.WriteLine(ordedFirst.Customer.FirstName);

            foreach (OrderDetail od in ordedFirst.OrderDetails)
            {
                Console.WriteLine(od.Product.Name);
                Console.WriteLine(od.Quantity);
            }

            var people = db.People.Include(p => p.Address);

            foreach (var personWithAddress in people)
            {
                Console.WriteLine($"{personWithAddress.Id} {personWithAddress.FirstName} {personWithAddress.LastName}");
                if (personWithAddress.Address != null)
                {
                    Console.WriteLine($"   Address: {personWithAddress.Address.Street}, {personWithAddress.Address.City}, {personWithAddress.Address.Country}");
                }

            }

            var managers = db.Managers.Include(p => p.Address);

            Console.WriteLine(managers.ToQueryString());

            foreach (var managerWithAddress in managers)
            {
                Console.WriteLine($"{managerWithAddress.FirstName} {managerWithAddress.LastName}");
                Console.WriteLine(managerWithAddress.Salary);
            }

            Console.WriteLine("Customers");
            var customers = db.Customers.Include(p => p.Address);

            Console.WriteLine(customers.ToQueryString());

            foreach (var customerWithAddress in customers)
            {
                Console.WriteLine($"{customerWithAddress.FirstName} {customerWithAddress.LastName}");
                Console.WriteLine(customerWithAddress.Discount);
            }

            #endregion
        }
    }
}
