namespace IntegrationApi.Model
{
    public class SensorHourSummaryModel
    {
        public DateTime DateTime { get; set; }

        public SensorReadingSummaryModel? Temperature { get; set; }

        public SensorReadingSummaryModel? Humidity { get; set; }

        public IEnumerable<SensorAlertModel>? Alerts { get; set; }
    }
}
