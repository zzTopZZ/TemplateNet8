using Catalog.Data.Context;
using Catalog.Domain.Contracts.Repositories;
using Catalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Data.Repositories
{
    public sealed class CategoryRepository(CatalogDbContext ctx) : GenericRepository<Category>(ctx), ICategoryRepository
    {
        public Task<IEnumerable<Category>> GetCategoriesWithProductsAsync(CancellationToken cancellationToken = default) 
        { 
            return Task.FromResult(ctx.Categories.Include(c => c.Products).AsEnumerable());
        }

    }
}
