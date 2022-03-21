CREATE PROCEDURE [dbo].[AddSensorReading]
	@SensorId INT,
	@Temperature DECIMAL(19,2),
	@Humidity DECIMAL(19,2)
AS
BEGIN
	INSERT INTO dbo.Sensor
		(
			SensorId,
			SensorName
		)
	SELECT	@SensorId AS SensorId,
			'Sensor ' + CONVERT(VARCHAR(50), @SensorId) AS SensorName
	WHERE	NOT EXISTS ( SELECT 1 FROM dbo.Sensor WHERE SensorId = @SensorId )

	INSERT INTO dbo.SensorReading
		(
			SensorId,
			ReadingDateTime,
			Temperature,
			Humidity
		)
	SELECT	@SensorId AS SensorId,
			GETUTCDATE() AS ReadingDateTime,
			@Temperature AS Temperature,
			@Humidity AS Humidity
END