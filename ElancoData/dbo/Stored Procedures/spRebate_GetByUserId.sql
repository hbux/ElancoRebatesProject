CREATE PROCEDURE [dbo].[spRebate_GetByUserId]
	@userId NVARCHAR(450)
AS
BEGIN
	SET NOCOUNT ON;

	SELECT [Id], [UserId], [OfferId], [InvoiceFileName], [CustomerFirstName], [CustomerLastName], 
	[CustomerEmail], [CustomerAddressLine1], [CustomerAddressLine2], [CustomerAddressLine3], [CustomerCity], 
	[CustomerState], [CustomerZipCode], [CustomerPhone], [PetName], [ClinicName], [ClinicAddressLine1], 
	[ClinicAddressLine2], [ClinicAddressLine3], [ClinicCity], [ClinicState], [ClinicZipCode], [AmountPurchased], 
	[DateSubmitted], [RebateStatus], [HasAccessed]
	FROM dbo.Rebate
	WHERE Rebate.UserId = @userId;
END