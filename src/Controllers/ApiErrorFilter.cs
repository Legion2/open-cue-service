using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace OpenCueService.Controllers
{
    public class ApiErrorFilter : IActionFilter, IOrderedFilter
    {
        public int Order { get; set; } = int.MaxValue - 10;

        public void OnActionExecuting(ActionExecutingContext context) { }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception is ApiError apiError)
            {
                context.Result = new ObjectResult(apiError.HttpMessage)
                {
                    StatusCode = apiError.StatusCode,
                };
                context.ExceptionHandled = true;
            }
        }
    }
}
