using Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataLoading
{
    internal class Program
    {
        public static void Main()
        {
            using var db = new ShopDbContext();   

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
                Console.WriteLine(o.Customer?.FirstName);
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

            Console.WriteLine(ordedFirst.Customer?.FirstName);

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

        }
    }
}
