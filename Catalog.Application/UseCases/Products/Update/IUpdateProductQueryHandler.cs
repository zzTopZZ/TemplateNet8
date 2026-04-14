using Catalog.Application.Contracts;

namespace Catalog.Application.UseCases.Products.Update
{
    public interface IUpdateProductQueryHandler : IRequestHandler<UpdateProductQuery, bool>;
}
