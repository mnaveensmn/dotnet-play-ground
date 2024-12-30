using Microsoft.AspNetCore.Mvc.Filters;

namespace webapi_play_ground.Filters;

public class TestAsyncActionFilter: IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        Console.WriteLine("TestAsyncActionFilter: Before the action");

        var executedContext = await next();
        
        Console.WriteLine("TestAsyncActionFilter: After the action");
    }
}