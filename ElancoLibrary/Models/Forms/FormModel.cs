using ElancoLibrary.Models.Rebates;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElancoLibrary.Models.Forms
{
    /// <summary>
    ///     This FormModel will be passed in as a parameter to add into a database of rebate submissions.
    ///     The UI FormModel values will be mapped into these properites after a valid submission.
    /// </summary>
    public class FormModel
    {
        public IBrowserFile Invoice { get; set; }
        public IProductModel Rebate { get; set; }
        public CustomerModel Customer { get; set; }
        public PetModel Pet { get; set; }
        public VeterinaryModel Veterinary { get; set; }
        public int AmountPurchased { get; set; }
        public DateTime Created { get; set; } = DateTime.UtcNow;
    }
}
