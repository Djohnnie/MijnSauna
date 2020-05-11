using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using MijnSauna.Backend.Common.Interfaces;
using MijnSauna.Backend.Model;

namespace MijnSauna.Backend.DataAccess
{
    [ExcludeFromCodeCoverage]
    public class DatabaseContext : DbContext
    {
        private readonly IConfigurationHelper _configurationHelper;

        public DbSet<ConfigurationValue> ConfigurationValues { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Sample> Samples { get; set; }
        public DbSet<Log> Logs { get; set; }

        public DatabaseContext(IConfigurationHelper configurationHelper)
        {
            _configurationHelper = configurationHelper;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (string.IsNullOrEmpty(_configurationHelper.ConnectionString))
            {
                optionsBuilder.UseInMemoryDatabase($"{_configurationHelper.Id}");
            }
            else
            {
                optionsBuilder.UseSqlServer(_configurationHelper.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ConfigurationValue>(e =>
            {
                e.ToTable("CONFIGURATION").HasKey(x => x.Id).IsClustered(false);
                e.Property(x => x.SysId).UseIdentityColumn();
                e.HasIndex(x => x.SysId).IsClustered();
                e.Property(p => p.Name).IsRequired();
                e.HasIndex(x => x.Name).IsUnique();
                e.Property(p => p.Value).IsRequired();
            });

            modelBuilder.Entity<Session>(e =>
            {
                e.ToTable("SESSIONS").HasKey(x => x.Id).IsClustered(false);
                e.Property(x => x.SysId).UseIdentityColumn();
                e.HasIndex(x => x.SysId).IsClustered();
            });

            modelBuilder.Entity<Sample>(e =>
            {
                e.ToTable("SAMPLES").HasKey(x => x.Id).IsClustered(false);
                e.Property(x => x.SysId).UseIdentityColumn();
                e.HasIndex(x => x.SysId).IsClustered();
            });

            modelBuilder.Entity<Log>(e =>
            {
                e.ToTable("LOGS").HasKey(x => x.Id).IsClustered(false);
                e.Property(x => x.SysId).UseIdentityColumn();
                e.HasIndex(x => x.SysId).IsClustered();
            });
        }
    }
}