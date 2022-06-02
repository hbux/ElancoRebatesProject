CREATE PROCEDURE [dbo].[spRebate_GetById]
	@submissionId NVARCHAR(450)
AS
BEGIN
	SET NOCOUNT ON;

	SELECT *
	FROM dbo.Rebate
	WHERE Rebate.Id = @submissionId;
END
