using Microsoft.AspNetCore.Mvc.Filters;

namespace webapi_play_ground.Filters;

public class TestAsyncActionFilterOne: IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        Console.WriteLine("TestAsyncActionFilterOne: Before the action");

        var executedContext = await next();
        
        Console.WriteLine("TestAsyncActionFilterOne: After the action");
    }
}