using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using TemplateNet8.Domain.Common;

namespace TemplateNet8.Api.Controllers.v1
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
