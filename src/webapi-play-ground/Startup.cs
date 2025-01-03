using webapi_play_ground.Extensions;
using webapi_play_ground.Filters;
using webapi_play_ground.Services;

namespace webapi_play_ground;

public class Startup(IConfiguration configuration)
{
    private IConfiguration Configuration { get; } = configuration;

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers().AddNewtonsoftJson();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddTransient<IService, Service>();
        services.AddTransient<IBookService, BookService>();
        services.AddSingleton(TimeProvider.System);
        services.AddMvcCore(options => options.Filters.Add(typeof(TestAsyncActionFilterOne)));
        services.AddMvcCore();
    }

    public static void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseSwagger();
        app.UseSwaggerUI();
        app.UseCustomMiddleware();
        app.UseRouting();
        app.UseAuthorization();
        app.UseEndpoints(endpoints => endpoints.MapControllers());
    }
}