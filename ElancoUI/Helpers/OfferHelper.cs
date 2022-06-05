using ElancoLibrary.Models.Offers;

namespace ElancoUI.Helpers
{
    public class OfferHelper : IOfferHelper
    {
        public List<OfferModel> FilterOffers(List<string> content, List<OfferModel> offers)
        {
            List<OfferModel> filteredOffers = new List<OfferModel>();

            foreach (OfferModel offer in offers)
            {
                foreach (ProductModel offerProduct in offer.Products)
                {
                    foreach (string text in content)
                    {
                        if (text == offerProduct.Name)
                        {
                            filteredOffers.Add(offer);
                        }
                    }
                }
            }

            return filteredOffers;
        }
    }
}
