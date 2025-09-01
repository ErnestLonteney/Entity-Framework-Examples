using Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Database.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> modelBuilder)
    {
        modelBuilder.HasKey(p => p.Id);
        modelBuilder.HasIndex(p => p.Name).IsUnique(false);
        modelBuilder.Property(product => product.Name).HasMaxLength(200);
        modelBuilder.Property(p => p.RawPrice).HasComputedColumnSql("(Price * 1.2)");
        modelBuilder.HasMany(p => p.OrderDetails).WithOne(s => s.Product).OnDelete(DeleteBehavior.NoAction);
    }
}
