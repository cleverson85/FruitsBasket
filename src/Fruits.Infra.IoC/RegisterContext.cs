using Fruits.Domain.Interfaces;
using Fruits.Infra.Data;
using Fruits.Infra.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Fruits.Infra.IoC
{
    public static class RegisterContext
    {
        public static IServiceCollection ConfigureContext(this IServiceCollection services, string connection) => 
            services.AddDbContext<Context>(options => options.UseNpgsql(connection))
                    .AddScoped<IUnitOfWork, UnitOfWork>();
    }
}
