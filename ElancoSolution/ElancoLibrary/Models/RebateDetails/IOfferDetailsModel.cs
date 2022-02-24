using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElancoLibrary.Models.RebateDetails
{
    public interface IOfferDetailsModel
    {
        string Name { get; set; }
        int Value { get; set; }
    }
}
