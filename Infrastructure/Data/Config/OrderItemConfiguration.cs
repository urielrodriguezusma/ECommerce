using Core.Entities.OrdersAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.OwnsOne(d => d.ItemOrdered, io => { io.WithOwner(); });
            builder.Property(d => d.Price)
                   .HasColumnType("decimal(18,2)");

        }
    }
}
