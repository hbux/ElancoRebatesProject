﻿CREATE TABLE [dbo].Brand
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[BrandName] NVARCHAR(128) NOT NULL,
	[ImageName] NVARCHAR(450) NOT NULL,
	[ImageType] NVARCHAR(10) NOT NULL,
	[AdditionalName] NVARCHAR(128) NULL
)
