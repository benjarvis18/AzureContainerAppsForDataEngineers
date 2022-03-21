namespace IntegrationApi.Model
{
    public class SensorSummaryResultModel
    {
        public int SensorId { get; set; }

        public string? SensorName { get; set; }

        public DateTime DateTime { get; set; }

        public decimal MinTemperature { get; set; }

        public decimal MaxTemperature { get; set; }

        public decimal AvgTemperature { get; set; }

        public decimal MinHumidity { get; set; }

        public decimal MaxHumidity { get; set; }

        public decimal AvgHumidity { get; set; }
    }
}
