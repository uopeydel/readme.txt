using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace Readme_Web.Controllers
{
    [Route("api/[controller]")]
    public class SampleDataController : Controller
    {
        //http://localhost:62454/api/SampleData/value
        [HttpGet("value")]
        public IActionResult value()
        {
            return Ok(Request.Host.Host + ":" + Request.Host.Port);
        }

        [HttpGet("callvalue")]
        public async Task<IActionResult> callvalue()
        {
            try
            {
                System.Net.Http.HttpClient client = new System.Net.Http.HttpClient();
                client.DefaultRequestHeaders.Clear();
                //client.DefaultRequestHeaders.Add("Authorization", "Bearer " + ChannelAccessToken);
                var data = await client.GetByteArrayAsync("http://localhost:63135/api/values");
                return Ok(JsonConvert.DeserializeObject<string[]>(Encoding.UTF8.GetString(data)));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        [HttpGet("[action]")]
        public IEnumerable<WeatherForecast> WeatherForecasts()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                DateFormatted = DateTime.Now.AddDays(index).ToString("d"),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            });
        }

        public class WeatherForecast
        {
            public string DateFormatted { get; set; }
            public int TemperatureC { get; set; }
            public string Summary { get; set; }

            public int TemperatureF
            {
                get
                {
                    return 32 + (int)(TemperatureC / 0.5556);
                }
            }
        }
    }
}
