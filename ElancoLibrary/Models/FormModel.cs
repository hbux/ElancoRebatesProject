using ElancoLibrary.Enums;

namespace ElancoLibrary.Models
{
    /// <summary>
    ///     A backend class representing each field in the form.
    /// </summary>
    public class FormModel
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public int OfferId { get; set; }
        public string InvoiceFileName { get; set; }
        public string CustomerFirstName { get; set; }
        public string CustomerLastName { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerAddressLine1 { get; set; }
        public string CustomerAddressLine2 { get; set; }
        public string CustomerAddressLine3 { get; set; }
        public string CustomerCity { get; set; }
        public string CustomerState { get; set; }
        public string CustomerZipCode { get; set; }
        public string CustomerPhone { get; set; }
        public string PetName { get; set; }
        public string ClinicName { get; set; }
        public string ClinicAddressLine1 { get; set; }
        public string ClinicAddressLine2 { get; set; }
        public string ClinicAddressLine3 { get; set; }
        public string ClinicCity { get; set; }
        public string ClinicState { get; set; }
        public string ClinicZipCode { get; set; }
        public int AmountPurchased { get; set; }
        public DateTime DateSubmitted { get; private set; } = DateTime.UtcNow;
        public Status RebateStatus { get; private set; } = Status.Submitted;
        public bool HasAccessed { get; set; } = false;
    }
}
