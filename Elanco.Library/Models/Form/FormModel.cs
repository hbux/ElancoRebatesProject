using ElancoUI.Models.Rebates;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elanco.Library.Models.Form
{
    public class FormModel
    {
        public IBrowserFile Invoice { get; set; }
        public IProductModel Rebate { get; set; }
        public CustomerModel Customer { get; set; }
        public PetModel Pet { get; set; }
        public VeterinaryModel Veterinary { get; set; }
        public int AmountPurchased { get; set; }
    }
}
