using ElancoLibrary.Models.Offers;
using ElancoUI.Models.DbContextModels;

namespace ElancoUI.Models
{
    public class FormDisplayInteractionModel
    {
        public List<Pet> Pets { get; set; } = new List<Pet>();
        public Pet PetSelected { get; set; }
        public OfferModel RebateSelected { get; set; }

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
    }
}
