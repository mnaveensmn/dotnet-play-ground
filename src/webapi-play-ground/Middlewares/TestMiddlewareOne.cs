namespace webapi_play_ground.Middlewares;

public class TestMiddlewareOne(RequestDelegate next)
{
    public async Task Invoke(HttpContext context)    
    {    
        try    
        {    
            Console.WriteLine("TestMiddlewareOne: Before Invoke");
            await next.Invoke(context);    
            Console.WriteLine("TestMiddlewareOne: After Invoke");
        }
        catch (Exception ex)    
        {    
            Console.WriteLine($"Exception Occured {ex.Message}");
        }   
    }    
}