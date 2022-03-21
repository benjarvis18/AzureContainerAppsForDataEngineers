CREATE TABLE [dbo].[SensorReading]
(
	[SensorReadingId] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[SensorId] INT NOT NULL FOREIGN KEY REFERENCES dbo.Sensor(SensorId),
	[ReadingDateTime] DATETIME NOT NULL,
	[Temperature] DECIMAL(19, 2),
	[Humidity] DECIMAL(19, 2)
)
