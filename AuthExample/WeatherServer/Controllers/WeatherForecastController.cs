using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WeatherServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet("Forecast")]
        [Authorize(Roles = "Common")]
        public JsonResult Forcast()
        {
            var rng = new Random();
            var result = Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index).Date.ToShortDateString(),
                DayOfWeek = DateTime.Now.AddDays(index).DayOfWeek.ToString(),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
            return new JsonResult(result);
        }

        [HttpGet("TodaysWeather")]
        [Authorize(Roles = "Admin")]
        public JsonResult TodaysWeather()
        {
            var rng = new Random();
            var result = new List<WeatherForecast>
            {
                new WeatherForecast
                {
                    Date = DateTime.Today.Date.ToShortDateString(),
                    DayOfWeek = DateTime.Today.DayOfWeek.ToString(),
                    TemperatureC = rng.Next(-20, 55),
                    Summary = Summaries[rng.Next(Summaries.Length)]
                }
            };
            return new JsonResult(result);
        }
    }
}
