using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Catalog.Domain.Common;

namespace Catalog.Api.Controllers.v2

{
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class TesteController : MainController   
    {
        [HttpGet]
        public IActionResult Get()
        {
            //return Ok("Template Api Teste v2 ");

            var dadoParaRetornar = new
            {
                Message = "Template Api Teste v2",
                Version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "Unknown"
            };

            var result = Result.Success(dadoParaRetornar);

            return CustomResponse(result);
        }
    }
}
