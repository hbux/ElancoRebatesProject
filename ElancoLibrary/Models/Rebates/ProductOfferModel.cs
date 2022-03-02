using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElancoLibrary.Models.Rebates
{
    public class ProductOfferModel : IProductOfferModel
    {
        public string Amount { get; set; }
        public int Value { get; set; }
    }
}
