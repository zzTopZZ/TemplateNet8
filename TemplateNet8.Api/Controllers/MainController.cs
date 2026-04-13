using Microsoft.AspNetCore.Mvc;
using TemplateNet8.Domain.Common;

namespace TemplateNet8.Api.Controllers
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
