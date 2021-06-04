using Fruits.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fruits.Infra.Data.Configurations
{
    class FruitConfiguration : IEntityTypeConfiguration<Fruit>
    {
        public void Configure(EntityTypeBuilder<Fruit> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Name)
               .IsRequired()
               .HasMaxLength(250);
            builder.Property(c => c.Description)
                .HasMaxLength(500);
            builder.Property(c => c.AvailableQuantity)
                .IsRequired()
                .HasPrecision(5);
            builder.Property(c => c.Price)
                .IsRequired()
                .HasPrecision(10, 2);
            builder.Property(c => c.Picture)
                .IsRequired();
        }
    }
}
