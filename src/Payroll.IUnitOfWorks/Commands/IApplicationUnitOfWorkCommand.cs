using System.Threading.Tasks;
using Payroll.IConnections.Bases;
using Payroll.IUnitOfWorks.Bases;

namespace Payroll.IUnitOfWorks.Commands
{
    public interface IApplicationUnitOfWorkCommand : IBaseUnitOfWork
    {
        IBaseDbContextCommand DbContextCommand { get; }
        Task<bool> CommitAsync(object executeBy);
        Task<bool> CommitAsync();
    }
}