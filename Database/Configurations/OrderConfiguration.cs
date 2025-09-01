using Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Database.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> modelBuilder)
    {
        modelBuilder.HasKey(o => o.Id).HasName("Orders_PK_Id");
        modelBuilder.Property(o => o.Id).ValueGeneratedOnAdd();
        modelBuilder
                .Property(o => o.Date)
                .HasColumnType("datetime") // not datetime2 as default
                                           // .HasDefaultValue(DateTime.Now) Does not work correctly with SQL Server. Why? 
                .HasDefaultValueSql("GETDATE()");
        modelBuilder
            .HasMany(p => p.OrderDetails)
            .WithOne(od => od.Order)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
