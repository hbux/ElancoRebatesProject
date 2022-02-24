using ElancoLibrary.Models.RebateDetails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElancoLibrary.Models.Rebates
{
    public class RebateOfferModel : IRebateOfferModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string OfferCode { get; set; }
        public DateOnly PurchaseBeginDate { get; set; }
        public DateOnly PurchaseDateEnd { get; set; }
        public List<string> ProductImages { get; set; } = new List<string>();
        public List<IOfferDetailsModel> OfferDetails { get; set; } = new List<IOfferDetailsModel>();
    }
}
