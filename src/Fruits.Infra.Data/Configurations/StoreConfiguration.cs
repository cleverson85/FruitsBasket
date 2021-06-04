using Fruits.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fruits.Infra.Data.Configurations
{
    class StoreConfiguration : IEntityTypeConfiguration<Store>
    {
        public void Configure(EntityTypeBuilder<Store> builder)
        {
            builder.HasKey(c => c.Id);
            builder.HasOne(c => c.Fruit);
            builder.Property(c => c.Quantity);
            builder.Property(c => c.TotalValue)
                .HasPrecision(10, 2);
        }
    }
}
