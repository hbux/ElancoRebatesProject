CREATE TABLE [dbo].[Offer]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[OfferCode] NVARCHAR(10) NOT NULL,
	[ValidPurchaseStart] DATETIME NOT NULL,
	[ValidPurchaseEnd] DATETIME2 NOT NULL,
	[AdditionalInformation] NVARCHAR(256) NOT NULL
)
