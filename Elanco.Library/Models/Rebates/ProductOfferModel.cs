namespace ElancoUI.Models.Rebates
{
    [Serializable]
    public class ProductOfferModel : IProductOfferModel
    {
        public string Amount { get; set; }
        public int Value { get; set; }
    }
}
