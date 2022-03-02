using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElancoLibrary.Models.Rebates
{
    public interface IProductOfferModel
    {
        string Amount { get; set; }
        int Value { get; set; }
    }
}
