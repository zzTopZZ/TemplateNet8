using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Catalog.Domain.Common;

namespace Catalog.Api.Controllers.v1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class TesteController : MainController
    {
        [HttpGet]
        public IActionResult Get()
        {
            var dadoParaRetornar = new
            {
                Message = "Template Api Teste v1",
                Version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "Unknown"
            };

            var result = Result.Success(dadoParaRetornar);

            return CustomResponse(result);
        }
    }
}
