using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestContractsDemo.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;

[assembly: ApiConventionType(typeof(CustomApiConventionsClean))]
namespace RestContractsDemo.Controllers
{
    public class Test
    {

    }

    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly IList<string> Summaries = new List<string>()
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet("{id}", Name = "GetById")]
        public IActionResult Get(string id)
        {
            if (string.IsNullOrEmpty(id))
                return BadRequest();

            var result = Summaries.Where(r => r.Equals(id));

            if (result.Any())
                return Ok(result);

            return NotFound();
        }

        [HttpPost]
        public IActionResult Post(string season)
        {
            try
            {
                if (string.IsNullOrEmpty(season))
                    return BadRequest();

                Summaries.Add(season);
                return Ok(Summaries.FirstOrDefault(r => r.Equals(season)));
            }
            catch (Exception ex)
            {
                return NotFound(ex);
            }
        }
    }
}
