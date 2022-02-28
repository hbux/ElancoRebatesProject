namespace ElancoUI.Models.Rebates
{
    public interface IProductModel
    {
        string Name { get; set; }
        string OfferCode { get; set; }
        List<string> Logos { get; set; }
        DateOnly ValidPurchasedStart { get; set; }
        DateOnly ValidPurchasedEnd { get; set; }
        List<IProductOfferModel> Offers { get; set; }
        string AdditionalInformation { get; set; }
    }
}
