using CacheUse.Infrastructure;
using CacheUse.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CacheUse.Controllers
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
        private readonly IMemoryCache memoryCache;
        private readonly IDistrubutedCacheService distrubutedCacheService;

        public WeatherForecastController(ILogger<WeatherForecastController> logger,IMemoryCache memoryCache, IDistrubutedCacheService distrubutedCacheService)
        {
            _logger = logger;
            this.memoryCache = memoryCache;
            this.distrubutedCacheService = distrubutedCacheService;
        }

        [HttpGet(Name = "GetMemoryCache")]
        public IEnumerable<WeatherForecast> GetMemoryCache()
        {
            var key = "weatherForecastList";

            if (!memoryCache.TryGetValue(key, out IEnumerable<WeatherForecast> weatherForcastList))
            {
                Console.WriteLine("weatherForecastList Not Exists in Cache ");
                weatherForcastList = Enumerable.Range(1, 5).Select(
                                                                    index => new WeatherForecast
                                                                    {
                                                                        Date = DateTime.Now.AddDays(index),
                                                                        TemperatureC = Random.Shared.Next(-20, 55),
                                                                        Summary = Summaries[Random.Shared.Next(Summaries.Length)]
                                                                    }
                                                                    );
                var opt = new MemoryCacheEntryOptions()
                {
                    SlidingExpiration = TimeSpan.FromSeconds(5),
                    AbsoluteExpiration = DateTime.Now.AddSeconds(30)
                };

                memoryCache.Set(key, weatherForcastList, opt);
                Console.WriteLine("weatherForecastList added Cache");
            }
            else
            {
                Console.WriteLine("weatherForecastList from Cache ");
            }
            return weatherForcastList.ToArray();
        }

      
        [HttpGet("{GetRedisCache}")]
        public  async Task<IEnumerable<WeatherForecast>> GetRedisCache()
        {
            var key = "weatherForecastList";
            IEnumerable<WeatherForecast>? weatherForecastList =null;

            weatherForecastList = await distrubutedCacheService.GetAsync<IEnumerable<WeatherForecast>>(key);

            if (weatherForecastList != null)
            {
                Console.WriteLine("weatherForecastList from Redis Cache ");

            }
            else
            {
                Console.WriteLine("weatherForecastList Not Exists in Redis Cache ");
                weatherForecastList = Enumerable.Range(1, 5).Select(
                                                                   index => new WeatherForecast
                                                                   {
                                                                       Date = DateTime.Now.AddDays(index),
                                                                       TemperatureC = Random.Shared.Next(-20, 55),
                                                                       Summary = Summaries[Random.Shared.Next(Summaries.Length)]
                                                                   }
                                                                   );

                
                await distrubutedCacheService.SetAsync<IEnumerable<WeatherForecast>>(key, weatherForecastList);
                Console.WriteLine("weatherForecastList added Cache");
            }


            return weatherForecastList;
        }
    }
}