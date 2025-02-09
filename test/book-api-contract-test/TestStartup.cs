using book_api.Models;
using book_api.Services;
using Microsoft.AspNetCore.Server.Kestrel.Core;

namespace book_api_contract_test;

public class TestStartup(IConfiguration configuration)
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<IBooksService, MockBookService>();
        services.AddControllers();
        services.AddCors();
        services.Configure<KestrelServerOptions>(options => { options.AllowSynchronousIO = true; });
    }

    public void Configure(IApplicationBuilder app)
    {
        app.UseCors(builder => builder.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());
        app.UseRouting();
        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
    }
}

public class MockBookService : IBooksService
{
    public List<Book> GetAllBooks()
    {
        return
        [
            new Book()
            {
                Id = "1",
                Name = "Book1",
                Author = "Author1"
            },

            new Book()
            {
                Id = "2",
                Name = "Book2",
                Author = "Author2"
            }
        ];
    }
}