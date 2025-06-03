using System.Net;
using Newtonsoft.Json;
using webapi_play_ground.Exceptions;

namespace webapi_play_ground.Middlewares;

public class ExceptionHandlerMiddleware    
{    
    private readonly RequestDelegate _next;    

    public ExceptionHandlerMiddleware(RequestDelegate next)    
    {    
        _next = next;    
    }    

    public async Task Invoke(HttpContext context)    
    {    
        try    
        {
            Console.WriteLine("ExceptionHandlerMiddleware: Before Invoke");
            await _next.Invoke(context);    
            Console.WriteLine("ExceptionHandlerMiddleware: After Invoke");
        }
        catch (Exception ex)    
        {    
            await HandleExceptionMessageAsync(context, ex, context.Response.StatusCode).ConfigureAwait(false);
        }   
    }    
    
    private static Task HandleExceptionMessageAsync(HttpContext context, Exception exception,
        int httpStatusCode = 0)
    {
        var result = JsonConvert.SerializeObject(new  
        {  
            StatusCode = httpStatusCode,  
            ErrorMessage = exception.Message  
        });  
        context.Response.ContentType = "application/json";  
        return context.Response.WriteAsync(result);  
    }  
}
    
    
