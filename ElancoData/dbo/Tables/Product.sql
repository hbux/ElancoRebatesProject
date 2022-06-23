CREATE TABLE [dbo].[Product]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[Name] NVARCHAR(50) NULL,
	[Amount] INT NULL,
	[AmountType] NVARCHAR(10) NULL,
	[DosageAmount] INT NULL,
	[DosageAmountMeasurementUnit] NVARCHAR(10) NULL,
	[AnimalType] NVARCHAR(50) NULL,
	[AnimalSizeMinimum] DECIMAL(18, 4) NULL,
	[AnimalSizeMaximum] DECIMAL(18, 4) NULL,
	[AnimalSizeMeasurementUnit] NVARCHAR(10) NULL,
	[BrandId] INT FOREIGN KEY REFERENCES Brand(Id) NOT NULL
)
