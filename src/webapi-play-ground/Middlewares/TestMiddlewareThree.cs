namespace webapi_play_ground.Middlewares;

public class TestMiddlewareThree(RequestDelegate next)
{
    public async Task Invoke(HttpContext context)    
    {    
        try    
        {    
            Console.WriteLine("TestMiddlewareThree: Before Invoke");
            await next.Invoke(context);    
            Console.WriteLine("TestMiddlewareThree: After Invoke");
        }
        catch (Exception ex)    
        {    
            Console.WriteLine($"Exception Occured {ex.Message}");
        }   
    }    
}