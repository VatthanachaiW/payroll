using System;
using System.Diagnostics.CodeAnalysis;
using Payroll.IConnections.Bases;
using Payroll.IUnitOfWorks.Bases;

namespace Payroll.UnitOfWorks.Bases
{
    public class BaseUnitOfWork : IBaseUnitOfWork
    {
        private bool _disposed;
        [ExcludeFromCodeCoverage] public IBaseDbContext Context { get; }

        public BaseUnitOfWork(IBaseDbContext context)
        {
            Context = context;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                Context.Dispose();
            }

            _disposed = true;
        }

        public virtual void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}