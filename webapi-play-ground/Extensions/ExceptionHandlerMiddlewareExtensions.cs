using webapi_play_ground.Middlewares;

namespace webapi_play_ground.Extensions;


public static class ExceptionHandlerMiddlewareExtensions  
{  
    public static void UseExceptionHandlerMiddleware(this IApplicationBuilder app)  
    {  
        app.UseMiddleware<ExceptionHandlerMiddleware>();  
    }  
}  