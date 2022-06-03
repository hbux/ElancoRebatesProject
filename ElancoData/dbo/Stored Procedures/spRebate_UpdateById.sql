CREATE PROCEDURE [dbo].[spRebate_UpdateById]
	@submissionId NVARCHAR(450)
AS
BEGIN
	SET NOCOUNT ON
	
	UPDATE dbo.Rebate
	SET Rebate.HasAccessed = 1
	WHERE Rebate.Id = @submissionId
END