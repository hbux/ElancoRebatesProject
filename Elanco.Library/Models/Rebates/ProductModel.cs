namespace ElancoUI.Models.Rebates
{
    public class ProductModel : IProductModel
    {
        public string Name { get; set; }
        public string OfferCode { get; set; }
        public List<string> Logos { get; set; } = new List<string>();
        public DateOnly ValidPurchasedStart { get; set; }
        public DateOnly ValidPurchasedEnd { get; set; }
        public List<IProductOfferModel> Offers { get; set; } = new List<IProductOfferModel>();
        public string AdditionalInformation { get; set; }
    }
}
