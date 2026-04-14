using Catalog.Data.Context;
using Catalog.Domain.Contracts.Repositories;
using Catalog.Domain.Entities;
using Catalog.Domain.Excepitions.Domain;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Data.Repositories
{
    public sealed class ProductRepository(CatalogDbContext ctx) : GenericRepository<Product>(ctx), IProductRepository
    {
        public override async Task<Product> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var data = await ctx.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);

            if (data == null) throw new EntityNotFoundException($"Product with id {id} not found.");

            return data;
        }

        public override async Task<IEnumerable<Product>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await ctx.Products
                .Include(p => p.Category)
                .ToListAsync(cancellationToken);
        }

        public Task<IEnumerable<Product>> GetProductsByCategoryAsync(int categoryId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
