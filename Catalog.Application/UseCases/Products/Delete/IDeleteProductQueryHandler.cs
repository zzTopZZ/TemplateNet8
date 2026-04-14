using Catalog.Application.Contracts;

namespace Catalog.Application.UseCases.Products.Delete
{
    public interface IDeleteProductQueryHandler : IRequestHandler<DeleteProductQuery, bool>;
}
