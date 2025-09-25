using Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace Database.Configurations
{
    public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> modelBuilder)
        {
            modelBuilder.HasKey(d => d.Id);
            modelBuilder.HasIndex(d => d.Name).IsUnique();
            // 100 - 100
            modelBuilder.HasMany(d => d.Managers).WithMany(m => m.Departments);
            modelBuilder.Property(d => d.Name).HasMaxLength(500).IsRequired();
        }
    }
}
