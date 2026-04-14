using Microsoft.AspNetCore.Mvc;
using Catalog.Domain.Common;

namespace Catalog.Api.Controllers
{
    [ApiController]
    public class MainController : ControllerBase
    {
        protected IActionResult CustomResponse(Result result)
        {
            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return BadRequest(new
            {
                success = false,
                errors = new[] { result.Error }
            });
        }
    }
}
