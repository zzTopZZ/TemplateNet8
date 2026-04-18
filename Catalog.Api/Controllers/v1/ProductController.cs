using Asp.Versioning;
using Catalog.Application.UseCases.Products;
using Catalog.Application.UseCases.Products.Create;
using Catalog.Application.UseCases.Products.Delete;
using Catalog.Application.UseCases.Products.GetById;
using Catalog.Application.UseCases.Products.ListAll;
using Catalog.Application.UseCases.Products.Update;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Api.Controllers.v1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ProductController(IProductService productService) : MainController 
    {
        [HttpGet("{id:int}", Name = nameof(GetByIdAsync))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            var result = await productService.HandleAsync(new GetByIdProductQuery(id), cancellationToken);
            return CustomResponse(result);
        }

        [HttpGet]
        //[Route("api/v{version:apiVersion}/products")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken)
        {
            var result = await productService.HandleAsync(new ListAllProductQuery(), cancellationToken);
            return CustomResponse(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CreateAsync([FromBody] CreateProductQuery query, CancellationToken cancellationToken)
        {
            var result = await productService.HandleAsync(query, cancellationToken);
            //return CustomResponse(result);
            return CreatedAtRoute(nameof(GetByIdAsync), new { id = result.Value.Id }, result);
        }

        [HttpPut("{id:int}", Name = nameof(UpdateAsync))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] UpdateProductQuery query, CancellationToken cancellationToken)
        {
            query.Id = id;
            var result = await productService.HandleAsync(query, cancellationToken);
            return CustomResponse(result);
        }

        [HttpDelete("{id:int}", Name = nameof(DeleteAsync))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteAsync(int id, CancellationToken cancellationToken)
        {
            var result = await productService.HandleAsync(new DeleteProductQuery(id), cancellationToken);
            return CustomResponse(result);
        }
    }
}
