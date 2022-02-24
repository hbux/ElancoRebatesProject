using ElancoLibrary.Models.RebateDetails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElancoLibrary.Models.Rebates
{
    public interface IRebateOfferModel
    {
        int Id { get; set; }
        string Name { get; set; }
        string OfferCode { get; set; }
        DateOnly PurchaseBeginDate { get; set; }
        DateOnly PurchaseDateEnd { get; set; }
        List<string> ProductImages  { get; set; }
        List<IOfferDetailsModel> OfferDetails { get; set; }
    }
}
