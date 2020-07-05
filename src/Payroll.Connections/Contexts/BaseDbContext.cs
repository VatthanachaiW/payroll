using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Payroll.Connections.Extensions;
using Payroll.Domains.Audits;
using Payroll.Domains.Masters;

namespace Payroll.Connections.Contexts
{
    [ExcludeFromCodeCoverage]
    public class BaseDbContext : IdentityDbContext<Profile, Role, int>
    {
        private bool _needCommit = false;
        public Guid UowId { get; set; }

        protected BaseDbContext(DbContextOptions<BaseDbContext> options) : base(options)
        {
            UowId = Guid.NewGuid();
        }

        protected BaseDbContext(DbContextOptions options) : base(options)
        {
            UowId = Guid.NewGuid();
        }

        public new IQueryable<T> Query<T>() where T : class
        {
            Database.ExecuteSqlRaw("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;");
            return Set<T>();
        }

        public override string ToString()
        {
            return UowId.ToString();
        }

        public async Task<bool> CommitAsyncForAdhocManager()
        {
            if (_needCommit)
            {
                return await this.CommitAsync() > -1;
            }

            return await Task.FromResult(true);
        }

        public async Task<IList<T>> ExecuteStoredProcedureAsync<T>(string storedProcedureName,
            Dictionary<string, object> parameters) where T : class
        {
            try
            {
                var dbCommand = Database.GetDbConnection().CreateCommand();
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.CommandText = storedProcedureName;
                if (parameters != null)
                {
                    foreach (var i in parameters)
                    {
                        var dbParameter = dbCommand.CreateParameter();
                        dbParameter.ParameterName = i.Key;
                        dbParameter.Value = i.Value ?? DBNull.Value;
                        dbCommand.Parameters.Add(dbParameter);
                    }
                }

                await Database.OpenConnectionAsync();
                var reader = await dbCommand.ExecuteReaderAsync();
                return reader.MapToList<T>();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            finally
            {
                await Database.CloseConnectionAsync();
            }
        }

        private bool _disposed;

        public override void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    base.Dispose();
                }
            }

            this._disposed = true;
        }
    }

    [ExcludeFromCodeCoverage]
    public abstract class BaseDbContext<T> : BaseDbContext where T : BaseDbContext
    {
        protected readonly IConfiguration Configuration;
        protected readonly DbContextOptions<T> ContextOptions;

        protected BaseDbContext(DbContextOptions<T> options) : base(options)
        {
            ContextOptions = options;
        }

        protected BaseDbContext(DbContextOptions<T> options, IConfiguration configuration) : base(options)
        {
            ContextOptions = options;
            Configuration = configuration;
        }

        [ExcludeFromCodeCoverage]
        private List<AuditTrailEntry> OnBeforeSaveChange()
        {
            ChangeTracker.DetectChanges();
            var auditEntries = new List<AuditTrailEntry>();
            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.Entity is AuditTrail || entry.State == EntityState.Detached ||
                    entry.State == EntityState.Unchanged) continue;

                var auditEntry = new AuditTrailEntry(entry)
                {
                    TableName = entry.Metadata.GetTableName(),
                    Action = Enum.GetName(typeof(EntityState), entry.State),
                };

                auditEntries.Add(auditEntry);

                foreach (var property in entry.Properties)
                {
                    if (property.IsTemporary)
                    {
                        auditEntry.TemporaryProperties.Add(property);
                        continue;
                    }

                    string propName = property.Metadata.Name;
                    if (property.Metadata.IsPrimaryKey())
                    {
                        auditEntry.KeyValues[propName] = property.CurrentValue;
                        continue;
                    }

                    switch (entry.State)
                    {
                        case EntityState.Added:
                            auditEntry.NewValues[propName] = property.CurrentValue;
                            break;
                        case EntityState.Deleted:
                            auditEntry.OldValues[propName] = property.OriginalValue ?? string.Empty;
                            break;
                        case EntityState.Modified:
                            if (property.IsModified)
                            {
                                auditEntry.OldValues[propName] = property.OriginalValue ?? string.Empty;
                                auditEntry.NewValues[propName] = property.CurrentValue;
                            }

                            break;
                    }
                }
            }

            foreach (var auditEntry in auditEntries.Where(_ => !_.HasTemporaryProperties))
            {
                Set<AuditTrail>().Add(auditEntry.ToAuditTrail());
            }

            return auditEntries.Where(_ => _.HasTemporaryProperties).ToList();
        }

        private bool _needCommit;

        public async Task<bool> CommitAsyncForAdhocManager(object executeBy)
        {
            if (_needCommit)
            {
                var result = await this.CommitAsync(executeBy);
                return result > -1;
            }

            return await Task.FromResult(true);
        }

        public async Task<bool> CommitAsync()
        {
            _needCommit = true;
            return await Task.FromResult(true);
        }

        [ExcludeFromCodeCoverage]
        private Task OnAfterSaveChangesAsync(List<AuditTrailEntry> auditEntries)
        {
            if (auditEntries == null || auditEntries.Count == 0) return Task.CompletedTask;

            foreach (var auditEntry in auditEntries)
            {
                foreach (var property in auditEntry.TemporaryProperties)
                {
                    if (property.Metadata.IsPrimaryKey())
                    {
                        auditEntry.KeyValues[property.Metadata.Name] = property.CurrentValue;
                    }
                    else
                    {
                        auditEntry.NewValues[property.Metadata.Name] = property.CurrentValue;
                    }
                }

                Set<AuditTrail>().Add(auditEntry.ToAuditTrail());
            }

            return SaveChangesAsync();
        }

        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
            CancellationToken cancellationToken = new CancellationToken())
        {
            var auditEntries = OnBeforeSaveChange();
            var result = await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken)
                .ConfigureAwait(false);
            await OnAfterSaveChangesAsync(auditEntries).ConfigureAwait(false);
            return result;
        }
    }
}