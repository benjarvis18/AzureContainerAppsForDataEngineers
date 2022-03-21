using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IngestApiData
{
    public class SensorResponse
    {
        public int SensorId { get; set; }

        public decimal Temperature { get; set; }

        public decimal Humidity { get; set; }
    }
}
