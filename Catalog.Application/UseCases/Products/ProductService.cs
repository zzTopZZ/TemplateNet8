using Catalog.Application.UseCases.Products.Create;
using Catalog.Application.UseCases.Products.Delete;
using Catalog.Application.UseCases.Products.GetById;
using Catalog.Application.UseCases.Products.ListAll;
using Catalog.Application.UseCases.Products.Update;
using Catalog.Domain.Common;
using Catalog.Domain.Contracts.Infra;
using Catalog.Domain.Contracts.Repositories;
using Catalog.Domain.Entities;

namespace Catalog.Application.UseCases.Products
{
    public class ProductService (IProductRepository productRepository, IUnitOfWork unitOfWork) : IProductService
    {
        public async Task<Result<IEnumerable<ProductResponse>>> HandleAsync(ListAllProductQuery request, CancellationToken cancellationToken = default)
        {
            var products = await productRepository.GetAllAsync(cancellationToken);
        
            if (!products.Any())
                return Result.Failure<IEnumerable<ProductResponse>>("Nenhuma produto encontrada");

            return Result.Success(products.Select(x => x.ToResponse()));

        }

        public async Task<Result<ProductResponse>> HandleAsync(GetByIdProductQuery request, CancellationToken cancellationToken = default)
        {
            var product = await productRepository.GetByIdAsync(request.Id, cancellationToken);

            if(product is null)
                return Result.Failure<ProductResponse>("Produto não encontrado");

            return Result.Success(product.ToResponse());
        }

        public async Task<Result<ProductResponse>> HandleAsync(CreateProductQuery request, CancellationToken cancellationToken = default)
        {
            var product = new Product(request.Name!, request.Price, (int)request.CategoryId!.Value);
            await productRepository.AddAsync(product, cancellationToken);
            await unitOfWork.CommitAsync(cancellationToken);
            return Result.Success(product.ToResponse());
        }

        public async Task<Result<bool>> HandleAsync(UpdateProductQuery request, CancellationToken cancellationToken = default)
        {
            var product = await productRepository.GetByIdAsync(request.Id, cancellationToken);

            if (product is null)
                return Result.Failure<bool>("Produto não encontrado");

            product.Update(request.Name!, (decimal)request.Price!);
            await productRepository.UpdateAsync(product, cancellationToken);
            await unitOfWork.CommitAsync(cancellationToken);
            return Result.Success(true);
        }

        public async Task<Result<bool>> HandleAsync(DeleteProductQuery request, CancellationToken cancellationToken = default)
        {
            var product = await productRepository.GetByIdAsync(request.Id, cancellationToken);

            if (product is null)
                return Result.Failure<bool>("Produto não encontrado");

            await productRepository.DeleteAsync(product, cancellationToken);
            await unitOfWork.CommitAsync(cancellationToken);
            return Result.Success(true);
        }
    }
}
