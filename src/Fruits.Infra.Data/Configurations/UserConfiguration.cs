using Fruits.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fruits.Infra.Data.Configurations
{
    class UserConfiguration : BaseConfiguration<User>
    {
        public override void Begin(EntityTypeBuilder<User> builder)
        {
            builder.Property(c => c.Email);
            builder.Property(c => c.Senha);
        }
    }
}
