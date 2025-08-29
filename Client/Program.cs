using Database.Entities;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace Client
{
    internal class Program
    {
        static void Main()
        {
            using var db = new ShopDbContext();

            //var customer = new Customer
            //{
            //    Name = "John Doe",
            //    Email = "john.doe@outlook.com"
            //};

            //var product1 = new Product
            //{
            //    Name = "Laptop",
            //    Description = "High performance laptop",
            //    Price = 1200.00m
            //};

            //var product2 = new Product
            //{
            //    Name = "Smartphone",
            //    Description = "Latest model smartphone",
            //    Price = 800.00m
            //};

            //var product3 = new Product("Keyboard", 22.45m);
            //product3.OrderDetailes.FirstOrDefault();

            //db.Products.AddRange(product1, product2, product3);
            //db.Customers.Add(customer);

            //var order = new Order
            //{
            //    Date = DateTime.Now,
            //    Customer = customer,
            //    Manager = new Manager
            //    {
            //        FirstName = "Alice",
            //        LastName = "Smith",
            //        Email = "smith@rozetka.com.ua"
            //    }
            //};

            // var manager11 = db.Managers.Find(order.ManagerId); 

            //var orderDetails = new List<OrderDetail>
            //{ // using constructor with parameters
            //    new() { Order = order, Product = product1, LineNumber = 1, Quantity = 2 },
            //    new() { Order = order, Product = product2, LineNumber = 2, Quantity = 50 }             
            //};

            //db.OrderDetailes.AddRange(orderDetails);


            //var dep1 = new Department
            //{
            //    Name = "It"
            //};

            //var dep2 = new Department
            //{
            //    Name = "QA"
            //};

            //var man1 = new Manager
            //{
            //    FirstName = "Ben",
            //    LastName = "Aflec",
            //};

            //var man2 = new Manager
            //{
            //    FirstName = "Olha",
            //    LastName = "Dovgan",
            //};

            //dep1.Managers.AddRange(man1, man2);
            //man2.Departments.Add(dep2);

            //db.Departments.AddRange(dep1, dep2);


            //var personnX = new Manager
            //{
            //    FirstName = "Joe",
            //    LastName = "Travison",
            //    Email = "mail@mail.com"
            //};

            //db.Managers.Add(personnX);  

            //db.SaveChanges();

           // var man11 =  db.Managers.Single(m => m.FirstName == "Joe" && m.LastName == "Travison");



            //man11.Address = new Address
            //{
            //    City = "Kyiv",
            //    Country = "Ukraine"               
            //};
            
            //db.SaveChanges();

            // Queries 


            // Egear Loading
            var ordersWithCustomer = db.Orders
                .Include(o => o.OrderDetailes) // підключаємо завантаження дочірніх записів 
                .ThenInclude(od => od.Product)
                .Where(o => o.Customer != null);

            Console.WriteLine(ordersWithCustomer.ToQueryString());

            foreach (Order o in ordersWithCustomer)
            {
                Console.WriteLine(o.Date);          // Lazy Loading
                Console.WriteLine(o.Customer.Name); // SELECT * FROM Customer WHERE Id = {o.CustomerId}
                Console.WriteLine(o.GetSum());
                                                            //  Lazy Loading
                foreach (OrderDetail od in o.OrderDetailes) // SELECT * FROM OrderDetails WHERE OrderId = {o.Id}
                {
                    Console.WriteLine(od.Product.Name);
                }
            }

            var ordedFirst = ordersWithCustomer.First();

            // Explixit loading
            db.Entry(ordedFirst)
                .Collection(o => o.OrderDetailes)
                .Query()
                .Where(od => od.Quantity > 10)
                .Load();

            db.Entry(ordedFirst)
               .Reference(o => o.Customer)
               .Load();

            Console.WriteLine(ordedFirst.Customer.Name);

            foreach (OrderDetail od in ordedFirst.OrderDetailes)
            {
                Console.WriteLine(od.Product.Name);
                Console.WriteLine(od.Quantity);
            }

        }
    }
}
