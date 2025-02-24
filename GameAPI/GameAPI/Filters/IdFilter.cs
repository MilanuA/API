using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace GameAPI.Filters;

public class IdFilter : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        if (!context.ActionArguments.TryGetValue("id", out object? actionArgument)) return;
        
        int id = (int)(actionArgument ?? -1);
            
        if (id > 0) return;
        
        context.Result = new BadRequestObjectResult(new
        {
            Message = "ID must be greater than 0",
            Field = "id"
        });
    }

    public void OnActionExecuted(ActionExecutedContext context) { }
}
