using Asp.Versioning;
using Catalog.Application.UseCases.Categories;
using Catalog.Application.UseCases.Categories.ListAll;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Api.Controllers.v1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class CategoriesController(ICategoryService categoryService) : MainController
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await categoryService.HandleAsync(new ListAllCategoryQuery(), cancellationToken);
            return CustomResponse(result);
        }
    }
}
