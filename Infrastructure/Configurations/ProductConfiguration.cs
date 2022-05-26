using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(d => d.Id).IsRequired();
            builder.Property(d => d.Name).IsRequired().HasMaxLength(120);
            builder.Property(d => d.Description).IsRequired().HasMaxLength(250);
            builder.Property(d => d.Price).HasColumnType("decimal(18,2)");
            builder.HasOne(d => d.ProductBrand).WithMany()
                   .HasForeignKey(d => d.ProductBrandId);

            builder.HasOne(d => d.ProductType).WithMany()
                    .HasForeignKey(d => d.ProductTypeId);
        }
    }
}
