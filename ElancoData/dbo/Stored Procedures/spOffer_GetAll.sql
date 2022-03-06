CREATE PROCEDURE [dbo].[spOffer_GetAll]
AS
BEGIN
	SET NOCOUNT ON;

	SELECT [Id], [OfferCode], [ValidPurchaseStart], [ValidPurchaseEnd], [AdditionalInformation] 
	FROM dbo.Offer
	ORDER BY OfferCode;
END