namespace webapi_play_ground.Middlewares;

public class TestMiddlewareTwo(RequestDelegate next)
{
    public async Task Invoke(HttpContext context)    
    {    
        try    
        {   
            Console.WriteLine("TestMiddlewareTwo: Before Invoke");
            await next.Invoke(context);    
            Console.WriteLine("TestMiddlewareTwo: After Invoke");
        }
        catch (Exception ex)    
        {    
            Console.WriteLine($"Exception Occured {ex.Message}");
        }   
    }    
}