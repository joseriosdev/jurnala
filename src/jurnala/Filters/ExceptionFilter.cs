using Domain.Models.Exception;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Presentation.Web.REST.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        private readonly IWebHostEnvironment _hostEnv;

        public ExceptionFilter(IWebHostEnvironment hostEnv)
        {
            _hostEnv = hostEnv;
        }

        public void OnException(ExceptionContext ctx)
        {
            string msn = $"The Application '{_hostEnv.ApplicationName}' failed with message: {ctx.Exception.Message}." +
                $"Of Type: {ctx.Exception.GetType()}";
            var error = new JurnalaError { StatusCode = "500", Message =  msn };
            ctx.Result = new JsonResult(error) { StatusCode = 500 };
        }
    }
}
