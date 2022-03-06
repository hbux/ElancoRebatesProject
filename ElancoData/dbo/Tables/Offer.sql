CREATE TABLE [dbo].[Offer]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[OfferCode] NVARCHAR(10) NOT NULL,
	[ValidPurchaseStart] DATETIME2 NOT NULL DEFAULT getutcdate(),
	[ValidPurchaseEnd] DATETIME2 NOT NULL DEFAULT getutcdate(),
	[AdditionalInformation] NVARCHAR(256) NOT NULL
)
