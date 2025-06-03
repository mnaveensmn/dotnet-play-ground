using Microsoft.AspNetCore.Mvc;

namespace nlog_exploration.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController(ILogger<WeatherForecastController> logger) : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        // Declare ILogger

        // Inject ILogger into the constructor

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            // Log messages at different levels
            logger.LogTrace("Trace log from WeatherForecastController.");
            logger.LogDebug("Debug log from WeatherForecastController. Request received.");
            logger.LogInformation("Getting weather forecast for {Count} days.", Summaries.Length);
            logger.LogWarning("This is a warning example.");

            try
            {
                throw new Exception("Simulated error during weather forecast retrieval.");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while generating weather forecast.");
            }

            var forecast = Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();

            logger.LogInformation("Weather forecast generated successfully.");
            return forecast;
        }
    }
}