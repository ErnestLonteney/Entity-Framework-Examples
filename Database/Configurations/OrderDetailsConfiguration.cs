using Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Database.Configurations
{
    public class OrderDetailsConfiguration : IEntityTypeConfiguration<OrderDetail>
    {
        public void Configure(EntityTypeBuilder<OrderDetail> builder)
        {
            builder.HasKey(od => new { od.OrderId, od.LineNumber }).HasName("PK_Id_LineNumber");
            builder.Property(od => od.LineNumber).ValueGeneratedOnAdd().HasDefaultValue(1);
            builder.HasOne(od => od.Order).WithMany(o => o.OrderDetails).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(od => od.Product).WithMany(p => p.OrderDetails).OnDelete(DeleteBehavior.NoAction);    
            builder.Property(od => od.Quantity).HasDefaultValue(1);
        }
    }
}
