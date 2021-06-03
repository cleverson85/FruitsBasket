using Fruits.Domain.Interfaces.Services;
using Fruits.Infra.Data.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Fruits.Infra.IoC
{
    public static class RegisterServices
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services) =>
            services.AddTransient<IFruitService, FruitService>()
                    .AddTransient<IUserService, UserService>()
                    .AddTransient<IAuthJwtService, AuthJwtService>();
    }
}
