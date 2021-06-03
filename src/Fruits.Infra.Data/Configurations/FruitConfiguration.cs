using Fruits.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fruits.Infra.Data.Configurations
{
    class FruitConfiguration : BaseConfiguration<Fruit>
    {
        public override void Begin(EntityTypeBuilder<Fruit> builder)
        {
            builder.Property(c => c.Name)
               .IsRequired()
               .HasMaxLength(250);
            builder.Property(c => c.Description)
                .HasMaxLength(500);
            builder.Property(c => c.AvailableQuantity)
                .IsRequired()
                .HasDefaultValueSql("integer");
            builder.Property(c => c.Price)
                .IsRequired()
                .HasDefaultValueSql("decimal");
            builder.Property(c => c.Picture)
                .IsRequired();
        }
    }
}
