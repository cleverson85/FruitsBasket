using Microsoft.Extensions.DependencyInjection;

namespace Fruits.Infra.IoC
{
    public static class RegisterCors
    {
        public static IServiceCollection ConfigureCors(this IServiceCollection services) =>
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                    builder.WithOrigins("http://localhost:4200", "http://localhost:5050", "http://localhost:80", "http://localhost:8080")
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                );
            });
    }
}
