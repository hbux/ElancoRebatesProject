CREATE PROCEDURE [dbo].[spRebate_Insert]
	@Id NVARCHAR(450),
	@UserId NVARCHAR(450),
	@OfferId int,
	@InvoiceFileName NVARCHAR(MAX),
	@CustomerFirstName NVARCHAR(50),
	@CustomerLastName NVARCHAR(50),
	@CustomerEmail NVARCHAR(50),
    @CustomerAddressLine1 NVARCHAR(100),
    @CustomerAddressLine2 NVARCHAR(100),
    @CustomerAddressLine3 NVARCHAR(100),
    @CustomerCity NVARCHAR(50),
    @CustomerState NVARCHAR(50),
    @CustomerZipCode NVARCHAR(10),
    @CustomerPhone NVARCHAR(20),
    @PetName NVARCHAR(50),
    @ClinicName NVARCHAR(100),
    @ClinicAddressLine1 NVARCHAR(100),
    @ClinicAddressLine2 NVARCHAR(100),
    @ClinicAddressLine3 NVARCHAR(100),
    @ClinicCity NVARCHAR(50),
    @ClinicState NVARCHAR(50),
    @ClinicZipCode NVARCHAR(10),
    @AmountPurchased int,
    @DateSubmitted DATETIME2,
    @RebateStatus NVARCHAR(20)
AS
BEGIN 
    SET NOCOUNT ON;

    INSERT INTO dbo.Rebate(Id, UserId, OfferId, InvoiceFileName, CustomerFirstName, CustomerLastName, CustomerEmail, 
        CustomerAddressLine1, CustomerAddressLine2, CustomerAddressLine3, CustomerCity, CustomerState, CustomerZipCode, CustomerPhone,
        PetName, ClinicName, ClinicAddressLine1, ClinicAddressLine2, ClinicAddressLine3, ClinicCity, ClinicState, ClinicZipCode,
        AmountPurchased, DateSubmitted, RebateStatus)
    VALUES (@Id, @UserId, @OfferId, @InvoiceFileName, @CustomerFirstName, @CustomerLastName, @CustomerEmail, 
        @CustomerAddressLine1, @CustomerAddressLine2, @CustomerAddressLine3, @CustomerCity, @CustomerState, @CustomerZipCode, 
        @CustomerPhone, @PetName, @ClinicName, @ClinicAddressLine1, @ClinicAddressLine2, @ClinicAddressLine3, @ClinicCity, 
        @ClinicState, @ClinicZipCode, @AmountPurchased, @DateSubmitted, @RebateStatus);
END