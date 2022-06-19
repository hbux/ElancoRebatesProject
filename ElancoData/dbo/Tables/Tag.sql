﻿CREATE TABLE [dbo].[Tag]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[Value] NVARCHAR(50) NOT NULL,
	[ProductId] INT FOREIGN KEY REFERENCES Product(Id)
)
