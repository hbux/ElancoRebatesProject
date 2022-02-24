using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElancoLibrary.Models.RebateDetails
{
    public class OfferDetailsModel : IOfferDetailsModel
    {
        public string Name { get; set; }
        public int Value { get; set; }
    }
}
