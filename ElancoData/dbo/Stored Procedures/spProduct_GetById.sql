CREATE PROCEDURE [dbo].[spProduct_GetById]
	@offerId int = 0
AS
BEGIN
    SET NOCOUNT ON;

    SELECT Product.Id, Product.[Name], Product.ImageName, Product.ImageType
    FROM dbo.Product
    INNER JOIN OfferProducts
    ON OfferProducts.ProductId = Product.Id
    WHERE OfferProducts.OfferId = @offerId
END