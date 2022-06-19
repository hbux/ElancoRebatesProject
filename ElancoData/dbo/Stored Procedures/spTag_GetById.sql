CREATE PROCEDURE [dbo].[spTag_GetById]
	@productId int = 0
AS
BEGIN
	SET NOCOUNT ON

	SELECT Tag.Id, Tag.[Value]
	FROM dbo.Tag
	WHERE Tag.ProductId = @productId;
END