using ElancoLibrary.Models.Offers;
using ElancoUI.Models.DbContextModels;
using Microsoft.AspNetCore.Components.Forms;
using System.ComponentModel.DataAnnotations;

namespace ElancoUI.Models
{
    public class FormInteractionModel
    {
        [Required]
        [Range(typeof(bool), "true", "true", ErrorMessage = "You must select a pet!")]
        public bool HasSelectedPet
        {
            get
            {
                return PetSelected != null;
            }
            set
            {
                HasSelectedPet = value;
            }
        }
        public List<Pet> Pets { get; set; } = new List<Pet>();
        public Pet PetSelected { get; set; }
        public string PetName { get; set; }

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
        public IBrowserFile InvoiceUploaded { get; set; }
        public string TrustedFileName { get; set; }

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
        public OfferModel RebateSelected { get; set; }
    }
}
