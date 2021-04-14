using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using OpenCue.Sdk;

namespace OpenCue.Service.Controllers
{
    public class SdkExceptionFilter : IActionFilter, IOrderedFilter
    {
        public int Order { get; set; } = int.MaxValue - 10;

        public void OnActionExecuting(ActionExecutingContext context) { }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception is SdkError exception)
            {
                context.Result = new ObjectResult(exception.Message)
                {
                    StatusCode = 400,
                };
                context.ExceptionHandled = true;
            }
        }
    }
}
