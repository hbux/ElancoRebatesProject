using ElancoLibrary.Models.Offers;
using ElancoUI.Models.DbContextModels;
using Microsoft.AspNetCore.Components.Forms;
using System.ComponentModel.DataAnnotations;

namespace ElancoUI.Models
{
    public class FormInteractionModel
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
