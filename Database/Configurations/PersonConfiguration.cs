using Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Database.Configurations;

public class PersonConfiguration : IEntityTypeConfiguration<Person>
{
    public void Configure(EntityTypeBuilder<Person> modelBuilder)
    {
        // 1- 1 relationship between Person and Address

        modelBuilder.HasKey(p => p.Id);   
        modelBuilder.Property(p => p.Id).ValueGeneratedOnAdd();
        modelBuilder.HasOne(p => p.Address)
                    .WithOne(a => a.Person) 
                    .IsRequired(false)
                    .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.HasIndex(p => new { p.FirstName, p.LastName }).IsUnique(false);
        modelBuilder.Property(p => p.FirstName).HasMaxLength(200);
        modelBuilder.Property(p => p.LastName).HasMaxLength(200);
        modelBuilder.Property(p => p.Email).HasMaxLength(300);
        modelBuilder.Property(p => p.Phone).HasMaxLength(15);

        modelBuilder
           // .UseTpcMappingStrategy()
            .ToTable("People");
    }
}
