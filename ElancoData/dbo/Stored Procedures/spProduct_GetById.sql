CREATE PROCEDURE [dbo].[spProduct_GetById]
	@brandId int = 0
AS
BEGIN
	SET NOCOUNT ON

	SELECT Product.Id, Product.[Name], Product.Amount, Product.AmountType, Product.SizeOfAmount, Product.SizeOfAmountType,
		   Product.PetType, Product.PetTypeSizeMinimum, Product.PetTypeSizeMaximum, Product.SizeType
	FROM dbo.Product
	WHERE Product.BrandId = @brandId
END