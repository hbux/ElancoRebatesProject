CREATE TABLE [dbo].[Product]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[Name] NVARCHAR(50) NULL,
	[Amount] INT NULL,
	[AmountType] NVARCHAR(10) NULL,
	[SizeOfAmount] INT NULL,
	[SizeOfAmountType] NVARCHAR(10) NULL,
	[PetType] NVARCHAR(50) NULL,
	[PetTypeSizeMinimum] DECIMAL NULL,
	[PetTypeSizeMaximum] DECIMAL NULL,
	[SizeType] NVARCHAR(10) NULL,
	[BrandId] INT FOREIGN KEY REFERENCES Brand(Id) NOT NULL
)
