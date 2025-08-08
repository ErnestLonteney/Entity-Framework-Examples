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
            //  modelBuilder.Entity<Product>().HasKey(p => p.Id).HasName("PK_Id");
            //   modelBuilder.Entity<Product>().HasKey(p => new { p.Id, p.Name });
            //  modelBuilder.Entity<Product>().Property(product => product.Name).HasMaxLength(200);

            //  modelBuilder.Entity<Order>().Ignore(o => o.Manager);
            //  modelBuilder.Ignore<Manager>();

            //   modelBuilder.Entity<Manager>().ToTable("SuperUsers");

            //  modelBuilder.Entity<Customer>().Property(c => c.PhoneNumber).HasColumnName("ContactNumber");

            modelBuilder.Entity<Product>().HasAlternateKey(p => p.Name);
            //modelBuilder.Entity<Manager>().HasAlternateKey(manager => new { manager.FirstName, manager.LastName });

            modelBuilder.Entity<Order>()
                .Property(o => o.Date)
                .HasColumnType("datetime")
                // .HasDefaultValue(DateTime.Now) Does not work correctly with SQL Server. Why? 
                .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<Product>().Property(p => p.RawPrice).HasComputedColumnSql("(Price * 1.2)");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost;Database=MyRozetka;Trusted_Connection=True;TrustServerCertificate=true");
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetailes { get; set; }
        public DbSet<Manager> Managers { get; set; }    

    }
}
