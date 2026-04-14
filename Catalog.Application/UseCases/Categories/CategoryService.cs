using Catalog.Application.Contracts;
using Catalog.Application.UseCases.Categories.ListAll;
using Catalog.Domain.Common;
using Catalog.Domain.Contracts.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application.UseCases.Categories
{
    public class CategoryService (ICategoryRepository categoryRepository) : ICategoryService
    {
        public async Task<Result<IEnumerable<CategoryResponse>>> HandleAsync(ListAllCategoryQuery request, CancellationToken cancellationToken = default)
        {
            var categories = await categoryRepository.GetAllAsync(cancellationToken);

            if (!categories.Any())
                return Result.Failure<IEnumerable<CategoryResponse>>("Nenhuma categoria encontrada");

            //return categories.Select(x => x.ToResponse());    
            return Result.Success(categories.Select(x => x.ToResponse()));

        }
    }
}
