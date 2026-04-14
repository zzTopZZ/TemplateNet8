using Catalog.Domain.Entities;

namespace Catalog.Domain.Contracts.Repositories
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<IEnumerable<Product>> GetProductsByCategoryAsync(int categoryId, CancellationToken cancellationToken = default);
    }
}
