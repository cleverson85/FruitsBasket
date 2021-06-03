using Fruits.Domain.Interfaces.Repositories;
using Fruits.Infra.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Fruits.Infra.IoC
{
    public static class RegisterRepositories
    {
        public static IServiceCollection ConfigureRepositories(this IServiceCollection services) =>
            services.AddScoped<IFruitRepository, FruitRepository>()
                    .AddScoped<IUserRepository, UserRepository>();
    }
}
