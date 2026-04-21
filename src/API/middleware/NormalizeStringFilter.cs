
using Microsoft.AspNetCore.Mvc.Filters;

namespace API.middleware
{
    public class NormalizeStringFilter: IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            foreach (var key in context.ActionArguments.Keys.ToList())
            {
                if (context.ActionArguments[key] is string value)
                {
                    context.ActionArguments[key] = Uri.UnescapeDataString(value);
                }
            }
        }
        public void OnActionExecuted(ActionExecutedContext context) { }
    }
}