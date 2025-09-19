using Database.Entities;

namespace Client
{
    internal class Program
    {
        static void Main()
        {
            using var db = new ShopDbContext();

            #region Addresses 
            var address1 = new Address() { Country = "Ukraine", City = "Dnipro", Street = "Veselaya" };
            var address2 = new Address() { Country = "Ukraine", City = "Dnipro", Street = "Rabochaya" };
            var address3 = new Address() { Country = "Ukraine", City = "Dnipro", Street = "Muzeyna" };
            var address4 = new Address() { Country = "Ukraine", City = "Dnipro", Street = "Chervona" };
            var address5 = new Address() { Country = "Ukraine", City = "Kyiv", Street = "Bulvarna" };
            var address6 = new Address() { Country = "Ukraine", City = "Kyiv", Street = "Peace" };
            var address7 = new Address() { Country = "Ukraine", City = "Kyiv", Street = "Fayvorite" };
            var address8 = new Address() { Country = "Ukraine", City = "Kyiv", Street = "Budivnikiv" };
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

            var manager1 = new Manager()
            {
                Address = address1,
                FirstName = "Emanuil",
                LastName = "Francisk",
                Departments = new List<Department>() { dep1 },
                Salary = 80000
            };

            var manager2 = new Manager()
            {
                Address = address2,
                FirstName = "Fillip",
                LastName = "Joe",
                Departments = new List<Department>() { dep1 },
                Salary = 100000
            };

            var manager3 = new Manager
            {
                FirstName = "Ben",
                LastName = "Aflec",
                Address = address7
            };

            var manager4 = new Manager
            {
                FirstName = "Olha",
                LastName = "Dovgan",
                Address = address8
            };

            manager3.Departments.Add(dep2);
            manager4.Departments.Add(dep2);

            var personX = new Manager
            {
                FirstName = "Joe",
                LastName = "Travison",
                Email = "mail@mail.com"
            };

            db.Managers.AddRange(manager1, manager2, manager3, manager4, personX);

            db.SaveChanges();

            #endregion

            #region Customers

            var customer = new Customer
            {
                FirstName = "John Doe",
                Email = "john.doe@outlook.com",
                Address = address6,
            };

            var customer1 = new Customer()
            {
                FirstName = "Andriy",
                LastName = "Biliy",
                Address = address3,
                Discount = 2f
            };

            var customer2 = new Customer()
            {
                FirstName = "Galina",
                LastName = "Martinova",
                Address = address4,
                Discount = 4f
            };

            var customer3 = new Customer()
            {
                FirstName = "Valentina",
                LastName = "Frolova",
                Address = address5,
                Discount = 5f
            };

            db.Customers.AddRange(customer, customer1, customer2, customer3);

            #endregion

            #region Products

            var product1 = new Product() { Name = "Notebook", Description = "Samsung", Price = 10000 };
            var product2 = new Product() { Name = "Notebook", Description = "Lenovo", Price = 8000 };
            var product3 = new Product() { Name = "Headphones", Description = "Samsung", Price = 500 };
            var product4 = new Product() { Name = "Headphones", Description = "Lenovo", Price = 300 };
            var product5 = new Product() { Name = "Headphones", Description = "Philips", Price = 900 };
            var product6 = new Product() { Name = "Mouse", Description = "Samsung", Price = 50 };
            var product7 = new Product() { Name = "Headphones", Description = "LenovoMax", Price = 600 };

            var product8 = new Product
            {
                Name = "Smartphone",
                Description = "Latest model smartphone",
                Price = 800.00m
            };

            var product9 = new Product("Keyboard", 22.45m);

            db.Products.AddRange(product1, product2, product3, product4, product5, product6, product7, product8, product9);

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
                },
                OrderDetails = new List<OrderDetail>
                {
                    new OrderDetail { Product = product8, Quantity = 1, LineNumber = 1 },
                    new OrderDetail { Product = product9, Quantity = 2, LineNumber = 2 }
                }
            };


            var order1 = new Order()
            {
                Customer = customer1,
                Date = new DateTime(2025, 05, 01),
                Manager = manager1
            };
            var order2 = new Order()
            {
                Customer = customer1,
                Date = new DateTime(2025, 08, 01),
                Manager = manager1
            };
            var order3 = new Order()
            {
                Customer = customer2,
                Date = new DateTime(2025, 03, 15),
                Manager = manager2
            };
            var order4 = new Order()
            {
                Customer = customer3,
                Date = new DateTime(2025, 04, 21),
                Manager = manager2
            };

            //OrderDetails
            var od1 = new OrderDetail() { Order = order1, LineNumber = 1, Product = product1, Quantity = 1 };
            var od2 = new OrderDetail() { Order = order1, LineNumber = 2, Product = product1, Quantity = 8 };
            var od3 = new OrderDetail() { Order = order2, LineNumber = 1, Product = product2, Quantity = 2 };
            var od4 = new OrderDetail() { Order = order2, LineNumber = 2, Product = product3, Quantity = 5 };
            var od5 = new OrderDetail() { Order = order3, LineNumber = 1, Product = product3, Quantity = 10 };
            var od6 = new OrderDetail() { Order = order4, LineNumber = 1, Product = product3, Quantity = 8 };

            db.Orders.AddRange(order);
            db.OrderDetails.AddRange(od1, od2, od3, od4, od5, od6);

            #endregion


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


            try
            {
                db.SaveChanges();
                Console.WriteLine("Data has been succesfully created");
                Console.WriteLine("Please comment Database.EnsureDeleted(); line in ShopDbContext");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
