namespace ElancoUI.Models.Rebates
{
    [Serializable]
    public class RebateProductModel : IRebateProductModel
    {
        public string Name { get; set; }
        public string OfferCode { get; set; }
        public List<string> Logos { get; set; } = new List<string>();
        public DateOnly ValidPurchasedStart { get; set; }
        public DateOnly ValidPurchasedEnd { get; set; }
        public List<IOfferModel> Offers { get; set; } = new List<IOfferModel>();
        public string AdditionalInformation { get; set; }
    }
}
