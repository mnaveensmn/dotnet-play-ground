using webapi_play_ground.Middlewares;

namespace webapi_play_ground.Extensions;


public static class CustomMiddlewareExtensions  
{  
    public static void UseCustomMiddleware(this IApplicationBuilder app)  
    {
        app.UseMiddleware<ExceptionHandlerMiddleware>();  
        app.UseMiddleware<TestMiddlewareOne>();  
        app.UseMiddleware<TestMiddlewareTwo>();  
        app.UseMiddleware<TestMiddlewareThree>();  
    }  
}  