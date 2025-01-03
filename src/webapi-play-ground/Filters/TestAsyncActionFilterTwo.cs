using Microsoft.AspNetCore.Mvc.Filters;

namespace webapi_play_ground.Filters;

public class TestAsyncActionFilterTwo: IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        Console.WriteLine("TestAsyncActionFilterTwo: Before the action");

        var executedContext = await next();
        
        Console.WriteLine("TestAsyncActionFilterTwo: After the action");
    }
}