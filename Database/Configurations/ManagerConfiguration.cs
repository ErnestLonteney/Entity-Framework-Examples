using Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Database.Configurations;

public class ManagerConfiguration : IEntityTypeConfiguration<Manager>
{
    public void Configure(EntityTypeBuilder<Manager> modelBuilder)
    {
        modelBuilder.ToTable("SuperUsers");
        modelBuilder.HasMany(p => p.Orders).WithOne(o => o.Manager).OnDelete(DeleteBehavior.NoAction);
        modelBuilder.HasMany(m => m.Departments).WithMany(d => d.Managers);
    }
}
