using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Payroll.IConnections.Bases
{
    public interface IBaseDbContextCommand : IBaseDbContextQuery
    {
        ChangeTracker ChangeTracker { get; }

        Task<bool> CommitAsyncForAdhocManager(object executeBy);
        Task<bool> CommitAsync();
    }
}