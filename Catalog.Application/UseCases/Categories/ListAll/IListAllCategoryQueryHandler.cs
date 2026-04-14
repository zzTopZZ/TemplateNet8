using Catalog.Application.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application.UseCases.Categories.ListAll
{
    public interface IListAllCategoryQueryHandler : IRequestHandler<ListAllCategoryQuery, IEnumerable<CategoryResponse>>;
}
