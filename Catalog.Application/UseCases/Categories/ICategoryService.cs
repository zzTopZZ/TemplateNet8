using Catalog.Application.UseCases.Categories.ListAll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application.UseCases.Categories
{
    public interface ICategoryService : IListAllCategoryQueryHandler;

}
