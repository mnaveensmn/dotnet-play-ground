namespace webapi_play_ground;

public class CustomEntryPoint
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Executed from CustomEntryPoint");
        CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
}