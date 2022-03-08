CREATE PROCEDURE [dbo].[spOfferDetails_GetById]
	@offerId int = 0
AS
BEGIN
	SET NOCOUNT ON;

	SELECT OfferDetails.Id, OfferDetails.Amount, OfferDetails.[Value]
	FROM dbo.OfferDetails 
	WHERE OfferDetails.OfferId = @offerId
END