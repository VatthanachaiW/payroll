using Payroll.IConnections.Bases;
using Payroll.IConnections.Queries;
using Payroll.IUnitOfWorks.Queries;
using Payroll.UnitOfWorks.Bases;

namespace Payroll.UnitOfWorks.Queries
{
    public class ApplicationUnitOfWorkQuery : BaseUnitOfWork, IApplicationUnitOfWorkQuery
    {
        public ApplicationUnitOfWorkQuery(IApplicationDbContextQuery query) : base(query)
        {
            DbContextQuery = query;
        }

        public IBaseDbContextQuery DbContextQuery { get; }
    }
}