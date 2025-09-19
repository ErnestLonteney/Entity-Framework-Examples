using Database.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Database.Entities
{
    public class ShopDbContext : DbContext
    {
        public ShopDbContext()         
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new PersonConfiguration());
            modelBuilder.ApplyConfiguration(new CustomerConfiguration());
            modelBuilder.ApplyConfiguration(new OrderConfiguration());
            modelBuilder.ApplyConfiguration(new OrderDetailsConfiguration());
            modelBuilder.ApplyConfiguration(new ManagerConfiguration());
            modelBuilder.ApplyConfiguration(new AddressConfiguration());
            modelBuilder.ApplyConfiguration(new DepartmentConfiguration());

            //  Fluent API configuration examples duplicated Data Annotations in this project

            // modelBuilder.Entity<Order>().Ignore(o => o.Manager);
            // modelBuilder.Ignore<Manager>();                            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Use your connection string here  
            optionsBuilder
             //   .UseLazyLoadingProxies()
                .UseSqlServer("Server=localhost;Database=MyRozetka;Trusted_Connection=True;TrustServerCertificate=true");
        }

        // Our tables in DataBase will be generated from these DbSet properties with the same names
        public DbSet<Product> Products { get; set; } // [Products]  
        public DbSet<Person> People { get; set; }
        public DbSet<Customer> Customers { get; set; } // [Customers]
        public DbSet<Order> Orders { get; set; } // [Orders]
        public DbSet<OrderDetail> OrderDetails { get; set; } // [OrderDetails]
        public DbSet<Manager> Managers { get; set; }  // [SuperUsers]  - because we set the table name in the Manager entity
        public DbSet<Address> Addresses { get; set; }   
        public DbSet<Department> Departments { get; set; }
    }
}
