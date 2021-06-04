using Fruits.Domain.Models;
using Fruits.Infra.Data.Configurations;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Fruits.Infra.Data.Contexts
{
    public class Context : DbContext
    {
        public DbSet<Fruit> Fruit { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Store> Store { get; set; }       

        public Context(DbContextOptions<Context> options) : base(options)
        { }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
