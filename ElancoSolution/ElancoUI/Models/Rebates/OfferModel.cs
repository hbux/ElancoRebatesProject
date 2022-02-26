namespace ElancoUI.Models.Rebates
{
    [Serializable]
    public class OfferModel : IOfferModel
    {
        public string Amount { get; set; }
        public int Value { get; set; }
    }
}
