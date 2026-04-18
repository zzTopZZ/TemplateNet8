using Catalog.Domain.Excepitions.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Catalog.Api.Infra.Filters
{
    public class CustonExceptionFilter (IHostEnvironment env) : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var details = new ProblemDetails();
            switch (context.Exception) 
            {
                case EntityNotFoundException ex:
                    details.Title = "Recurso não encontrado";
                    details.Detail = ex.Message;
                    details.Status = StatusCodes.Status404NotFound;
                    //context.Result = new NotFoundObjectResult(details);
                    break;
                default:
                    details.Title = "Ocorreu um erro inesperado";
                    if (env.IsDevelopment()) 
                    {
                        details.Detail = context.Exception.Message;
                        details.Extensions["StackTrace"] = context.Exception.StackTrace;
                    }
                    details.Status = StatusCodes.Status500InternalServerError;
                    details.Type = "InternalServerError";
                    break;
            };
            context.ExceptionHandled = true;
            context.Result = new ObjectResult(details)
            { 
                StatusCode = details.Status 
            };            
        }
    }
}
