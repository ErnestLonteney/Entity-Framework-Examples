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
            // Fluent API configuration examples - duplicated Data Annotations in this project
            //  modelBuilder.Entity<Product>().HasKey(p => p.Id).HasName("PK_Id");
            //  modelBuilder.Entity<Product>().HasKey(p => new { p.Id, p.Name });
            //  modelBuilder.Entity<Product>().Property(product => product.Name).HasMaxLength(200);
            //  modelBuilder.Entity<Order>().Ignore(o => o.Manager);
            //  modelBuilder.Ignore<Manager>();
            //  modelBuilder.Entity<Manager>().ToTable("SuperUsers");
            //  modelBuilder.Entity<Customer>().Property(c => c.PhoneNumber).HasColumnName("ContactNumber");
            //  modelBuilder.Entity<Product>().HasAlternateKey(p => p.Name);
            //modelBuilder.Entity<Manager>().HasAlternateKey(manager => new { manager.FirstName, manager.LastName });

            modelBuilder.Entity<Order>()
                .Property(o => o.Date)
                .HasColumnType("datetime") // not datetime2 as default
                // .HasDefaultValue(DateTime.Now) Does not work correctly with SQL Server. Why? 
                .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<Product>().Property(p => p.RawPrice).HasComputedColumnSql("(Price * 1.2)");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Use your connection string here  
            optionsBuilder.UseSqlServer("Server=localhost;Database=MyRozetka;Trusted_Connection=True;TrustServerCertificate=true");
        }

        // Our tables in DataBase will be generated from these DbSet properties with the same names
        public DbSet<Product> Products { get; set; } // [Products]
        public DbSet<Customer> Customers { get; set; } // [Customers]
        public DbSet<Order> Orders { get; set; } // [Orders]
        public DbSet<OrderDetail> OrderDetailes { get; set; } // [OrderDetailes]
        public DbSet<Manager> Managers { get; set; }  // [SuperUsers]  - because we set the table name in the Manager entity
    }
}
