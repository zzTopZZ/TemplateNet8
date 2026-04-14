using Catalog.Application.Contracts;

namespace Catalog.Application.UseCases.Products.Create
{
    public interface ICreateProductQueryHandler : IRequestHandler<CreateProductQuery, ProductResponse>;
}
