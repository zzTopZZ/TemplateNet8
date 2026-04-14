using Catalog.Data.Context;
using Catalog.Domain.Contracts.Infra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Data.Repositories
{
    public class UnitOfWork(CatalogDbContext ctx) : IUnitOfWork
    {
        public Task CommitAsync(CancellationToken cancellationToken = default)
            => ctx.SaveChangesAsync(cancellationToken);

        public Task RollbackAsync(CancellationToken cancellationToken = default)
            => Task.CompletedTask;
    }
}
