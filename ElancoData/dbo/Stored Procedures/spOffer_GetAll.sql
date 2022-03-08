CREATE PROCEDURE [dbo].[spOffer_GetAll]
AS
BEGIN
	SET NOCOUNT ON;

	SELECT Offer.Id, Offer.OfferCode, Offer.ValidPurchaseStart, Offer.ValidPurchaseEnd, Offer.AdditionalInformation
	FROM dbo.Offer
END