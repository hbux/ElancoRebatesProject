using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElancoLibrary.Models.Rebates
{
    public interface IProductModel
    {
        int Id { get; set; }
        string Name { get; set; }
        string OfferCode { get; set; }
        List<string> Logos { get; set; }
        DateOnly ValidPurchasedStart { get; set; }
        DateOnly ValidPurchasedEnd { get; set; }
        List<IProductOfferModel> Offers { get; set; }
        string AdditionalInformation { get; set; }
    }
}
