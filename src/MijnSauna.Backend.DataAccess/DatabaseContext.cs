using System;
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

        public DbSet<Session> Sessions { get; set; }

        public DbSet<Sample> Samples { get; set; }

        public DatabaseContext(IConfigurationHelper configurationHelper)
        {
            _configurationHelper = configurationHelper;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (String.IsNullOrEmpty(_configurationHelper.ConnectionString))
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
            modelBuilder.Entity<Session>(e =>
            {
                e.ToTable("SESSIONS").HasKey(x => x.Id).ForSqlServerIsClustered(false);
                e.Property<Int32>("SysId").UseSqlServerIdentityColumn();
                e.HasIndex("SysId").ForSqlServerIsClustered();
            });

            modelBuilder.Entity<Sample>(e =>
            {
                e.ToTable("SAMPLES").HasKey(x => x.Id).ForSqlServerIsClustered(false);
                e.Property<Int32>("SysId").UseSqlServerIdentityColumn();
                e.HasIndex("SysId").ForSqlServerIsClustered();
            });
        }
    }
}