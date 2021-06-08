
using FluentValidation.AspNetCore;
using Fruits.API.Middlewares;
using Fruits.Application.Filters;
using Fruits.Domain.Settings;
using Fruits.Infra.IoC;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using System;
using System.Linq;

namespace Fruits.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            AppSettings appSettings = new AppSettings(Configuration);
            services.AddSingleton(options => appSettings);

            services
                .ConfigureContext(appSettings.ConnectionStringDefault)
                .ConfigureServices()
                .ConfigureCors()
                .ConfigureRepositories();

            services
               .AddControllers(options => options.Filters.Add(typeof(ModelValidationFilter)))
               .AddNewtonsoftJson(options =>
               {
                   options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                   options.UseCamelCasing(false);
               })
               .AddFluentValidation(options => options.RegisterValidatorsFromAssemblyContaining<Startup>());

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "Fruit Basket Api", Version = "v1" });
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme (Example: 'Bearer 12345abcdefdsfsfddsds')",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
                options.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

            }

            app
             .UseSwagger()
             .UseSwaggerUI(options =>
             {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
             });

            app
              .UseHttpsRedirection()
              .UseStaticFiles()
              .UseRouting()
              .UseCors("CorsPolicy")
              .UseMiddleware(typeof(JwtMiddleware))
              .UseMiddleware(typeof(ErrorHandlingMiddleware))
              .UseAuthentication()
              .UseAuthorization()
              .UseEndpoints(options =>
              {
                  options.MapControllers();
              });
        }
    }
}
