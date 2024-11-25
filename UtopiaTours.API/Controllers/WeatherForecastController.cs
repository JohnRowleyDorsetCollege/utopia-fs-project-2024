using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using UtopiaTours.API;

namespace UtopiaTours.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        private readonly IMemoryCache _cache;
        public WeatherForecastController(ILogger<WeatherForecastController> logger, IMemoryCache cache)
        {
            _logger = logger;
            _cache = cache;
        }

        [Authorize]
        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpPost(Name = "GetWeatherForecastFromCache")]
        public IActionResult GetCache()
        {
            Console.WriteLine("CAching");
         
            if (!_cache.TryGetValue("WeatherData", out WeatherForecast[] weatherData))
            {
               
                var rng = new Random();
                weatherData = Enumerable.Range(1, 5).Select(index => new WeatherForecast
                {
                    Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                    TemperatureC = Random.Shared.Next(-20, 55),
                    Summary = Summaries[Random.Shared.Next(Summaries.Length)]
                })
             .ToArray();


            }

            var cacheOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1)
            };
           
            Console.WriteLine(weatherData);
         
            _cache.Set("WeatherData", weatherData, cacheOptions);


            return Ok(weatherData);
        }
    }
}
