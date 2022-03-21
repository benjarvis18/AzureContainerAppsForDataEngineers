namespace IntegrationApi.Model
{
    public class SensorSummaryModel
    {
        public int SensorId { get; set; }

        public string? SensorName { get; set; }

        public IEnumerable<SensorHourSummaryModel>? Data { get; set; }
    }
}
