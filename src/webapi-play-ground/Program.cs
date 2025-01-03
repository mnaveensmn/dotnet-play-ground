using webapi_play_ground.Extensions;
using webapi_play_ground.Filters;
using webapi_play_ground.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<TestAsyncActionFilterOne>();
builder.Services.AddControllers(options =>
{
    options.Filters.Add(typeof(TestAsyncActionFilterTwo));
}).AddNewtonsoftJson();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IService, Service>();
builder.Services.AddTransient<IBookService, BookService>();
builder.Services.AddSingleton(TimeProvider.System);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCustomMiddleware();
app.UseRouting();
app.UseAuthorization();
app.MapControllers();

app.Run();