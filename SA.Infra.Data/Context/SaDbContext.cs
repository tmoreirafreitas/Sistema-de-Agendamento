using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SA.Domain.Entities;
using SA.Infra.Data.Mapping;
using System.IO;

namespace SA.Infra.Data.Context
{
    public class SaDbContext : DbContext
    {
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Processo> Processos { get; set; }

        public SaDbContext(DbContextOptions<SaDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {            
            modelBuilder.ApplyConfiguration(new ClienteMap());
            modelBuilder.ApplyConfiguration(new ProcessoMap());
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // get the configuration from the app settings
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            // define the database to use
            optionsBuilder.
                UseInMemoryDatabase
                (config.GetConnectionString("DefaultConnection"));
        }
    }
}
