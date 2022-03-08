CREATE PROCEDURE [dbo].[spOffer_GetAll]
AS
BEGIN
	SET NOCOUNT ON;

	SELECT Offer.Id, Offer.OfferCode, Offer.ValidPurchaseStart, Offer.ValidPurchaseEnd, Offer.AdditionalInformation,
		Product.Id, Product.[Name], Product.ImageName, Product.ImageType
	FROM dbo.Offer
	INNER JOIN OfferProducts ON OfferProducts.OfferId = Offer.id
	INNER JOIN Product ON Product.Id = OfferProducts.ProductId
END