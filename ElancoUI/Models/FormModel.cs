using ElancoLibrary.Models.Offers;
using ElancoUI.Models.DbContextModels;
using Microsoft.AspNetCore.Components.Forms;
using System.ComponentModel.DataAnnotations;

namespace ElancoUI.Models
{
    /// <summary>
    ///     Form properites which represent each field on the user interface. These properites are bound to
    ///     the user interface input fields.
    /// </summary>
    public class FormModel
    {
        public List<Pet> Pets { get; set; } = new List<Pet>();

        [Required]
        [Range(typeof(bool), "true", "true", ErrorMessage = "You must upload an invoice!")]
        public Pet PetSelected { get; set; }

        [Required(ErrorMessage = "Pet name cannot be empty!")]
        public string PetName { get; set; }


        public IBrowserFile InvoiceUploaded { get; set; }
        public OfferModel RebateSelected { get; set; }

        [Required]
        [Range(typeof(bool), "true", "true", ErrorMessage = "You must upload an invoice!")]
        public bool HasUploadedInvoice
        { 
            get
            {
                return InvoiceUploaded != null;
            }
            set
            {
                HasUploadedInvoice = value;
            }
        }

        [Required]
        [Range(typeof(bool), "true", "true", ErrorMessage = "You must select a rebate!")]
        public bool HasSelectedRebate
        {
            get
            {
                return RebateSelected != null;
            }
            set
            {
                HasSelectedRebate = value;
            }
        }


        [Required(ErrorMessage = "First name cannot be empty!")]
        public string CustomerFirstName { get; set; }

        [Required(ErrorMessage = "Last name cannot be empty!")]
        public string CustomerLastName { get; set; }

        [Required(ErrorMessage = "Address cannot be empty!")]
        public string CustomerAddress { get; set; }

        [Required(ErrorMessage = "City cannot be empty!")]
        public string CustomerCity { get; set; }

        [Required(ErrorMessage = "State cannot be empty!")]
        public string CustomerState { get; set; }

        [Required(ErrorMessage = "Zip code cannot be empty!")]
        public string CustomerZipCode { get; set; }

        [Required(ErrorMessage = "Phone cannot be empty!")]
        [DataType(DataType.PhoneNumber)]
        public string CustomerPhone { get; set; }

        [Required(ErrorMessage = "Email address cannot be empty!")]
        [DataType(DataType.EmailAddress)]
        public string CustomerEmailAddress { get; set; }

        [Required(ErrorMessage = "Email confirmation cannot be empty!")]
        [DataType(DataType.EmailAddress)]
        [Compare("CustomerEmailAddress", ErrorMessage = "Email addresses do not match!")]
        public string CustomerEmailConfirmation { get; set; }


        [Required(ErrorMessage = "Clinic name cannot be empty!")]
        public string ClinicName { get; set; }

        [Required(ErrorMessage = "Clinic address cannot be empty!")]
        public string ClinicAddress { get; set; }

        [Required(ErrorMessage = "Clinic city cannot be empty!")]
        public string ClinicCity { get; set; }

        [Required(ErrorMessage = "Clinic state cannot be empty!")]
        public string ClinicState { get; set; }

        [Required(ErrorMessage = "Clinic zip code cannot be empty!")]
        public string ClinicZipCode { get; set; }


        [Required(ErrorMessage = "Amount purchased cannot be empty!")]
        [Range(1, int.MaxValue, ErrorMessage = "Amount purchased must be at least 1!")]
        public int AmountPurchased { get; set; }
    }
}
