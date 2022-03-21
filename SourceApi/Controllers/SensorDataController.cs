using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SourceApi.Controllers
{
    public class SensorResponse
    {
        public int SensorId { get; set; }

        public decimal Temperature { get; set; }

        public decimal Humidity { get; set; }
    }

    [Route("api/sensor-data")]
    [ApiController]
    public class SensorDataController : ControllerBase
    {
        public IEnumerable<SensorResponse> Get()
        {
            return Enumerable.Range(1, 10).Select(index => new SensorResponse()
            {
                SensorId = index,
                Temperature = Random.Shared.Next(15, 30),
                Humidity = Random.Shared.Next(70, 100),
            });
        }
    }
}
