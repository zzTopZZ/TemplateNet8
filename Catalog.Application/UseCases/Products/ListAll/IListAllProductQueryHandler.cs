using Catalog.Application.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application.UseCases.Products.ListAll
{
    public interface IListAllProductQueryHandler : IRequestHandler<ListAllProductQuery, IEnumerable<ProductResponse>>;
}
