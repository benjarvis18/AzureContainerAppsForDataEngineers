using IntegrationApi.Model;
using Dapper;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using System.Data.SqlClient;

namespace IntegrationApi.Controllers
{
    [Route("api/sensor-summary")]
    [ApiController]
    public class SensorSummary : ControllerBase
    {
        const string SQL_CONNECTION_STRING = "{Connection String}";

        private async Task<SqlConnection> GetSqlConnectionAsync()
        {
            var sqlConnection = new SqlConnection(SQL_CONNECTION_STRING);
            await sqlConnection.OpenAsync();

            return sqlConnection;
        }

        public async Task<IEnumerable<SensorSummaryModel>> Get()
        {
            using var sql = await GetSqlConnectionAsync();

            var summary = (await sql.QueryAsync<SensorSummaryResultModel>("dbo.GetSensorReadingSummary")).GroupBy(s => new { s.SensorId, s.SensorName });
            var result = new List<SensorSummaryModel>();

            foreach (var sensor in summary)
            {
                var sensorSummary = new SensorSummaryModel()
                {
                    SensorId = sensor.Key.SensorId,
                    SensorName = sensor.Key.SensorName
                };

                var minutes = new List<SensorHourSummaryModel>();

                foreach (var minute in sensor)
                {
                    var sensorHourSummary = new SensorHourSummaryModel()
                    {
                        DateTime = minute.DateTime,
                        Humidity = new SensorReadingSummaryModel()
                        {
                            Minimum = minute.MinHumidity,
                            Maximum = minute.MaxHumidity,
                            Average = minute.AvgHumidity
                        },
                        Temperature = new SensorReadingSummaryModel()
                        {
                            Minimum = minute.MinTemperature,
                            Maximum = minute.MaxTemperature,
                            Average = minute.AvgTemperature
                        }
                    };

                    var alerts = new List<SensorAlertModel>();

                    if (sensorHourSummary.Temperature.Maximum > 27)
                    {
                        alerts.Add(new SensorAlertModel()
                        {
                            Severity = "Critical",
                            Message = $"Maximum temperature ({sensorHourSummary.Temperature.Maximum}) was above 27°C"
                        });
                    }

                    if (sensorHourSummary.Temperature.Average < 23 || sensorHourSummary.Temperature.Average > 25)
                    {
                        alerts.Add(new SensorAlertModel()
                        {
                            Severity = "Critical",
                            Message = $"Average temperature ({sensorHourSummary.Temperature.Average}) was outside of the desired range (23°C - 25°C)"
                        });
                    }

                    sensorHourSummary.Alerts = alerts;
                    minutes.Add(sensorHourSummary);
                }

                sensorSummary.Data = minutes;
                result.Add(sensorSummary);
            }

            return result;
        }
    }
}
