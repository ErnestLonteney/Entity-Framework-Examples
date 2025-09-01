using Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Database.Configurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> modelBuilder)
        {
            modelBuilder.HasMany(p => p.Orders).WithOne(o => o.Customer).OnDelete(DeleteBehavior.SetNull);
            modelBuilder.Property(c => c.Discount).HasDefaultValue(0);
            modelBuilder.Property(c => c.Phone).HasColumnName("ContactNumber");
            modelBuilder.ToTable("Customers");
        }
    }
}
