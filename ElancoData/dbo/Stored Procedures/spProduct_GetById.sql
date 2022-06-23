CREATE PROCEDURE [dbo].[spProduct_GetById]
	@brandId int = 0
AS
BEGIN
	SET NOCOUNT ON

	SELECT Product.Id, Product.[Name], Product.Amount, Product.AmountType, Product.[DosageAmount], Product.[DosageAmountMeasurementUnit],
		   Product.[AnimalType], Product.[AnimalSizeMinimum], Product.[AnimalSizeMaximum], Product.[AnimalSizeMeasurementUnit]
	FROM dbo.Product
	WHERE Product.BrandId = @brandId
END