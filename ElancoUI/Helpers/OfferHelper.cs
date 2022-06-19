using ElancoLibrary.Models.Offers;

namespace ElancoUI.Helpers
{
    public class OfferHelper : IOfferHelper
    {
        private ILogger<OfferHelper> _logger;

        public OfferHelper(ILogger<OfferHelper> logger)
        {
            _logger = logger;
        }

        public List<OfferModel> FilterOffers(List<string> content, List<OfferModel> offers)
        {
            HashSet<OfferModel> filteredOffers = new HashSet<OfferModel>();

            foreach (string analysedText in content)
            {
                foreach (OfferModel offer in offers)
                {
                    foreach (ProductModel product in offer.Products)
                    {
                        foreach (TagModel tag in product.Tags)
                        {
                            if (analysedText.ToLower() == tag.Value.ToLower())
                            {
                                filteredOffers.Add(offer);
                            }
                        }
                    }
                }
            }

            if (filteredOffers.Count == 0)
            {
                _logger.LogDebug("No matching offers found by analysed product image at {Time}", DateTime.UtcNow);
            }
            else
            {
                _logger.LogDebug("Matched {Count} offers with the analysed product image at {Time}", filteredOffers.Count, DateTime.UtcNow);
            }

            return filteredOffers.ToList();
        }
    }
}
