CREATE PROCEDURE [dbo].[GetSensorReadingSummary]
AS
BEGIN
	SELECT	S.SensorId,
			S.SensorName,
			DATETIMEFROMPARTS(YEAR(SR.ReadingDateTime), MONTH(SR.ReadingDateTime), DAY(SR.ReadingDateTime), DATEPART(HOUR, SR.ReadingDateTime), DATEPART(MINUTE, SR.ReadingDateTime), 0, 0) AS [DateTime],
			MIN(SR.Temperature) AS MinTemperature,
			MAX(SR.Temperature) AS MaxTemperature,
			AVG(SR.Temperature) AS AvgTemperature,
			MIN(SR.Humidity) AS MinHumidity,
			MAX(SR.Humidity) AS MaxHumidity,
			AVG(SR.Humidity) AS AvgHumidity
	FROM	dbo.Sensor S
			INNER JOIN dbo.SensorReading SR ON SR.SensorId = S.SensorId
	GROUP BY
		S.SensorId,
		S.SensorName,
		DATETIMEFROMPARTS(YEAR(SR.ReadingDateTime), MONTH(SR.ReadingDateTime), DAY(SR.ReadingDateTime), DATEPART(HOUR, SR.ReadingDateTime), DATEPART(MINUTE, SR.ReadingDateTime), 0, 0)
END