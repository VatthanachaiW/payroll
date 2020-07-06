using System.Threading.Tasks;
using Payroll.IConnections.Bases;
using Payroll.IConnections.Commands;
using Payroll.IUnitOfWorks.Commands;
using Payroll.UnitOfWorks.Bases;

namespace Payroll.UnitOfWorks.Commands
{
    public class ApplicationUnitOfWorkCommand : BaseUnitOfWork, IApplicationUnitOfWorkCommand
    {
        public ApplicationUnitOfWorkCommand(IApplicationDbContextCommand context) : base(context)
        {
            DbContextCommand = context;
        }

        public IBaseDbContextCommand DbContextCommand { get; }


        public async Task<bool> CommitAsync(object executeBy) => await DbContextCommand.CommitAsyncForAdhocManager(executeBy);
        public async Task<bool> CommitAsync() => await DbContextCommand.CommitAsync();
    }
}