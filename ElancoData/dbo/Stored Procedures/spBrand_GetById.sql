CREATE PROCEDURE [dbo].[spBrand_GetById]
	@offerId int = 0
AS
BEGIN
    SET NOCOUNT ON;

    SELECT Brand.Id, Brand.[BrandName], Brand.ImageName, Brand.ImageType
    FROM dbo.Brand
    INNER JOIN OfferProducts
    ON OfferProducts.BrandId = Brand.Id
    WHERE OfferProducts.OfferId = @offerId
END