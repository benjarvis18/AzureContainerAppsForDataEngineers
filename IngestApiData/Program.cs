using System.Data;
using System.Data.SqlClient;

using Dapper;

using Flurl.Http;

using IngestApiData;

const string API_URL = "https://app-container-apps-demo-01.azurewebsites.net/api/sensor-data";
const string SQL_CONNECTION_STRING = "{Connection String}";

async Task<SqlConnection> GetSqlConnectionAsync()
{
    var sqlConnection = new SqlConnection(SQL_CONNECTION_STRING);
    await sqlConnection.OpenAsync();

    return sqlConnection;
}

while(true)
{
    Console.WriteLine($"Ingesting data from {API_URL}");

    var apiResponse = await API_URL.GetJsonAsync<IEnumerable<SensorResponse>>();

    using (var sql = await GetSqlConnectionAsync())
    {
        foreach (var sensor in apiResponse)
        {
            Console.WriteLine($"Adding data for sensor {sensor.SensorId} to Azure SQL DB");

            await sql.ExecuteAsync("dbo.AddSensorReading", param: sensor, commandType: CommandType.StoredProcedure);
        }
    }

    Console.WriteLine("Waiting 10 seconds before ingesting again...");
    await Task.Delay(10000);
}