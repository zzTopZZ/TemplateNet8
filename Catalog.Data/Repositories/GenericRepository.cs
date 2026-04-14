using Catalog.Data.Context;
using Catalog.Domain.Contracts.Repositories;
using Catalog.Domain.Entities;
using Catalog.Domain.Excepitions.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Data.Repositories
{
    public class GenericRepository<TEntity>(CatalogDbContext ctx) : RepositoryBase, IGenericRepository<TEntity> where TEntity : EntityBase
    {
        public virtual async Task<TEntity> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var data = await ctx.Set<TEntity>().FindAsync(new object[] { id }, cancellationToken);

            if (data == null) throw new EntityNotFoundException($"Entity of type {typeof(TEntity).Name} with id {id} not found.");

            return data;
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var data = await ctx.Set<TEntity>().AsNoTracking().ToListAsync(cancellationToken); 
            
            return data;    
        }

        public async Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            await ctx.Set<TEntity>().AddAsync(entity, cancellationToken);
        }
        public async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            Task.FromResult(ctx.Set<TEntity>().Update(entity));
        }

        public async Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            Task.FromResult(ctx.Set<TEntity>().Remove(entity));
        }

    }
}
