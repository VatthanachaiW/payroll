using Payroll.IConnections.Bases;
using Payroll.IUnitOfWorks.Bases;

namespace Payroll.IUnitOfWorks.Queries
{
    public interface IApplicationUnitOfWorkQuery:IBaseUnitOfWork
    {
        IBaseDbContextQuery DbContextQuery { get; }
    }
}