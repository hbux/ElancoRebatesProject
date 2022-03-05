using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElancoLibrary.Models.Offers
{
    public class OfferModel
    {
        public int Id { get; set; }
        public string OfferCode { get; set; }
        public DateTime ValidPurchaseStart { get; set; }
        public DateTime ValidPurchaseEnd { get; set; }
        public List<ProductModel> Products { get; set; } = new List<ProductModel>();
        public List<OfferDetails> Details { get; set; } = new List<OfferDetails>();
        public string AdditionalInformation { get; set; }
    }
}
