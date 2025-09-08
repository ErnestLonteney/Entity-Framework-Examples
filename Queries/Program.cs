using Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace Queries
{
    internal class Program
    {
        static void Main()
        {
            using var context = new ShopDbContext();

            // Filters 
            var products = context.Products
                .Where(p => (p.Price > 100 && p.Price < 1000) ||
                      (p.Price == null))
                .Where(p => p.Description != null && p.Description.Contains("laptop"));

            Console.WriteLine(products.ToQueryString());

            foreach (var product in products)
            {
                Console.WriteLine($"{product.Name} - {product.Price}");
            }

            Console.WriteLine(new String('-', 40));

            // Orders 
            var orders = context.Orders.Include(o => o.OrderDetails).ThenInclude(od => od.Product)
                .Where(o => o.Date <= DateTime.Now && o.Date > new DateTime(2025, 1, 1))
                .OrderByDescending(o => o.Date);

            var orders2 = from order in context.Orders.Include(o => o.OrderDetails).ThenInclude(od => od.Product)
                          where order.Date <= DateTime.Now && order.Date > new DateTime(2025, 1, 1)
                          orderby order.Date descending
                          select order;

            Console.WriteLine(orders.ToQueryString());

            Console.WriteLine(new String('-', 40));

            // Projection
            var result = context.Orders
                .Include(o => o.Manager)
                .Include(o => o.Customer)
            .Where(o => o.Date <= DateTime.Now && o.Date > new DateTime(2025, 1, 1))
            .Where(o => o.Customer != null)
            .Select(o => new SalesResult
            {            
                ManagerName = o.Manager.FirstName + " " + o.Manager.LastName,
                CustomerName = o.Customer!.FirstName + " " + o.Customer.LastName,
                TotalAmount = o.OrderDetails.Sum(od => (od.Product.Price ?? 0) * od.Quantity)
            });

            Console.WriteLine(result.ToQueryString());

            Console.WriteLine(new String('-', 40));

            foreach (var item in result)
            {
                Console.WriteLine($"{item.ManagerName} sold to {item.CustomerName} for {item.TotalAmount}");
            }

            var result2 = from o in context.Orders
                .Include(o => o.Manager)
                .Include(o => o.Customer)
                         where o.Date <= DateTime.Now && o.Date > new DateTime(2025, 1, 1)
                         where o.Customer != null
                         select new
                          {
                              ManagerName = o.Manager.FirstName + " " + o.Manager.LastName,
                              CustomerName = o.Customer!.FirstName + " " + o.Customer.LastName,
                              TotalAmount = o.OrderDetails.Sum(od => (od.Product.Price ?? 0) * od.Quantity)
                          };

            Console.WriteLine(result.ToQueryString());

            foreach (var item in result)
            {
                Console.WriteLine($"{item.ManagerName} sold to {item.CustomerName} for {item.TotalAmount}");
            }

            Console.WriteLine(new String('-', 40));

            // Grouping

           var groups = context.Orders.Include(o => o.Manager).Include(o => o.OrderDetails).ThenInclude(od => od.Product)
                   .Where(o => o.Date <= DateTime.Now && o.Date > new DateTime(2025, 1, 1))
                   .GroupBy(o => o.Manager);

            Console.WriteLine(groups.ToQueryString());

            foreach (var group in groups)
            {
                Console.WriteLine($"Manager: {group.Key.FirstName} {group.Key.LastName}");
                foreach (var order in group)
                {
                    var sum = order.OrderDetails.Sum(od => (od.Product.Price ?? 0) * od.Quantity);
                    Console.WriteLine($"\tOrder {order.Id}, Date: {order.Date}, Total: {sum}");
                }
            }


            var groups2 = from o in context.Orders.Include(o => o.Manager).Include(o => o.OrderDetails).ThenInclude(od => od.Product)
                          where o.Date <= DateTime.Now && o.Date > new DateTime(2025, 1, 1)
                          group o by o.Manager;

            Console.WriteLine(groups.ToQueryString());

            foreach (var group in groups)
            {
                Console.WriteLine($"Manager: {group.Key.FirstName} {group.Key.LastName}");
                foreach (var order in group)
                {
                    var sum = order.OrderDetails.Sum(od => (od.Product.Price ?? 0) * od.Quantity);
                    Console.WriteLine($"\tOrder {order.Id}, Date: {order.Date}, Total: {sum}");
                }
            }

            Console.WriteLine(new String('-', 40));

            // Aggregation

            // SELECT COUNT(*) FROM Orders WHERE DATE <= GETDATE() AND DATE > '2025-01-01'

            var totalSales = context.Orders.Count(o => o.Date <= DateTime.Now && o.Date > new DateTime(2025, 1, 1));

            var totalSales2 = context.Orders.Where(o => o.Date <= DateTime.Now && o.Date > new DateTime(2025, 1, 1))
                .Sum(o => o.OrderDetails.Sum(od => (od.Product.Price ?? 0) * od.Quantity));

            var totalSalesAv = context.Orders.Where(o => o.Date <= DateTime.Now && o.Date > new DateTime(2025, 1, 1))
                .Average(o => o.OrderDetails.Sum(od => (od.Product.Price ?? 0) * od.Quantity));

            var cheaperProdut = context.Products.Min(p => p.Price);
            var expensiveProduct = context.Products.Max(p => p.Price);

            // IQueryable vs IEnumerable

            // LINQ to Obkects (LOOPS)
            var list = new[] { new Product(), new Product() };
            IEnumerable<Product> query = list.Where(p => p.Price > 100).OrderBy(p => p.Price); // IEnumerable  

            // LINQ to Entities (SQL QUERY)
            IQueryable<Product> query2 = context.Products.Where(p => p.Price > 100).OrderBy(p => p.Price); // IQueryable  

            Console.WriteLine(query2.ToQueryString());

            IEnumerable<Product> query3 = context.Products.Where(p => p.Price > 100).OrderBy(p => p.Price).ToList();

            context.Products.Take(10).ToList(); // SQL: SELECT TOP(10) * FROM Products

            context.Products.Skip(5).Take(10).ToList(); // SQL: SELECT * FROM Products ORDER BY [some column] OFFSET 5 ROWS FETCH NEXT 10 ROWS ONLY


            var join = context.Products.Join(context.OrderDetails,
                    p => p.Id,
                    od => od.Product.Id,
                    (p, od) => new { p.Name, od.Quantity });

            Console.WriteLine(join.ToQueryString());

            Console.WriteLine(new string('-', 20));

            var leftJoin = context.Products.LeftJoin(context.OrderDetails,
                    p => p.Id,
                    od => od.Product.Id,
                    (p, od) => new { p.Name, od.Quantity });

            Console.WriteLine(leftJoin.ToQueryString());
        }
    }
}
