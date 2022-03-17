using System.ComponentModel.DataAnnotations;

namespace ElancoUI.Models
{
    public class UnAuthenticatedFormModel
    {
        [Required(ErrorMessage = "Pet name cannot be empty!")]
        public string PetName { get; set; }

        public FormModel Form { get; set; }
    }
}
