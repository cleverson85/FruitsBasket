using AutoFixture;
using Fruits.Domain.Models;
using Fruits.Infra.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using BC = BCrypt.Net.BCrypt;

namespace Fruits.Infra.IoC
{
    public static class Migration
    {
        public static IHost MigrateDbContext<TContext>(this IHost host) where TContext : DbContext
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetService<TContext>();
                context.Database.Migrate();
#if DEBUG
               InserirDadosParaTeste(context as Context);
#endif
            }

            return host;
        }
        private static void InserirDadosParaTeste(Context context)
        {
            context.Add(BuildUser());
            context.SaveChanges();
        }

        private static User BuildUser()
        {
            var fixture = new Fixture();
            var user = fixture.Build<User>()
                .With(c => c.Email, "usuario@admin.com.br")
                .With(c => c.Senha, BC.HashPassword("123456"))
                .Create();

            return user;
        }
    }
}
