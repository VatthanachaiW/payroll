using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Payroll.Connections.Mappings;
using Payroll.Domains.Audits;
using Payroll.IConnections.Commands;
using Payroll.IConnections.Queries;

namespace Payroll.Connections.Contexts
{
    public class ApplicationDbContext : BaseDbContext<ApplicationDbContext>, IApplicationDbContextQuery, IApplicationDbContextCommand
    {
        ~ApplicationDbContext()
        {
            Dispose(false);
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IConfiguration configuration) : base(options, configuration)
        {
        }

        [ExcludeFromCodeCoverage]
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string connectionString = Environment.GetEnvironmentVariable("DB_Connection")?.Trim() ?? Configuration.GetConnectionString("DefaultConnection");

                optionsBuilder
                    .UseLazyLoadingProxies()
                    .ConfigureWarnings(warnings => warnings.Throw(RelationalEventId.QueryPossibleExceptionWithAggregateOperatorWarning))
                    .UseNpgsql(connectionString, sqlOptions =>
                    {
                        sqlOptions.EnableRetryOnFailure(5, TimeSpan.FromSeconds(30), null!);
                        sqlOptions.MigrationsAssembly(typeof(ApplicationDbContext).GetTypeInfo().Assembly.GetName().Name);
                    });
            }

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AuditTrail>(AuditTailTableMapper.Config);
            base.OnModelCreating(modelBuilder);
        }
    }
}