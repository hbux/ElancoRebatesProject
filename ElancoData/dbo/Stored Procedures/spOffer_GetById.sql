CREATE PROCEDURE [dbo].[spOffer_GetById]
	@offerId int = 0
AS
BEGIN
	SET NOCOUNT ON

	SELECT * FROM dbo.Offer
	WHERE Offer.Id = @offerId;
END
