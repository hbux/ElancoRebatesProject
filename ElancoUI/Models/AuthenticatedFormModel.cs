using ElancoUI.Models.DbContextModels;
using System.ComponentModel.DataAnnotations;

namespace ElancoUI.Models
{
    public class AuthenticatedFormModel
    {
        public List<Pet> Pets { get; set; } = new List<Pet>();

        [Required]
        [Range(typeof(bool), "true", "true", ErrorMessage = "You must upload an invoice!")]
        public Pet PetSelected { get; set; }

        public bool HasSelectedPet
        {
            get
            {
                return PetSelected != null;
            }
        }

        public FormModel Form { get; set; }
    }
}
