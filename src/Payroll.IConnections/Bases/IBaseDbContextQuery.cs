using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Payroll.IConnections.Bases
{
    public interface IBaseDbContextQuery : IBaseDbContext
    {
        DatabaseFacade Database { get; }
        DbSet<T> Set<T>() where T : class;
        EntityEntry Entry(object entity);
    }
}