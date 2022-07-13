using ElancoLibrary.Data;
using ElancoLibrary.Models.Brands;
using ElancoLibrary.Models.Offers;
using Microsoft.Extensions.Logging;

namespace ElancoLibrary.Filters
{
    public class OfferFilter : IOfferFilter
    {
        private readonly ILogger<OfferFilter> _logger;
        private readonly IOfferData _offerData;
        private List<OfferModel> _allOffers;

        public OfferFilter(ILogger<OfferFilter> logger, IOfferData offerData)
        {
            _logger = logger;
            _offerData = offerData;

            GetAllOffers();
        }

        /// <summary>
        ///     Automatically matches an offer based on the analysis of the uploaded product image. Attempts to match key words against 
        ///     product names, tags and offer details. The offer which has the most accuracy is automatically selected provided
        ///     the minimal criteria is met.
        /// </summary>
        /// <param name="analysedContent">A list of text the API extracted from the product image.</param>
        /// <param name="selectOfferCallback">The delegate which automatically selects the offer (found on Rebates page UI)</param>
        /// <exception cref="NullReferenceException">If no matches are made this exception is thrown to notify the user.</exception>
        public void AutoMatchOffer(List<string> analysedContent, Action<OfferModel> selectOfferCallback)
        {
            // 1. Match analysed content to a product
            // 2. Match product to an offer

            Dictionary<OfferModel, Accuracy> offersDetected = new Dictionary<OfferModel, Accuracy>();

            foreach (OfferModel offer in _allOffers)
            {
                foreach (BrandModel product in offer.Brands)
                {
                    // Splits each product name by spaces into its own string to more accurately match an offer
                    List<string> productNameWords = product.BrandName.Split().ToList();

                    foreach (string productWord in productNameWords)
                    {
                        // Validate if the analysed text matches any product name
                        if (analysedContent.Any(x => x.Contains(productWord)))
                        {
                            // Minimum criteria has been met
                            if (offersDetected.ContainsKey(offer) == true)
                            {
                                // Increase the accuracy rating if offer is inside dictionary
                                // Also sets met criteria to true
                                offersDetected[offer].AccuracyRating++;
                                offersDetected[offer].HasMetMinimumCriteria = true;
                            }
                            else
                            {
                                // Increase the accuracy rating if offer is not inside dictionary
                                // and sets minimum criteria to true
                                offersDetected.Add(offer, new Accuracy());
                                offersDetected[offer].HasMetMinimumCriteria = true;
                            }
                        }
                    }
                }

                // More validation via the offer details to further increase accuracy
                foreach (OfferDetailsModel details in offer.Details)
                {
                    List<string> detailsWords = details.Amount.ToLower().Split().ToList();

                    foreach (string word in detailsWords)
                    {
                        if (analysedContent.Any(x => x.ToLower().Contains(word.ToLower())) == true)
                        {
                            if (offersDetected.ContainsKey(offer) == true)
                            {
                                offersDetected[offer].AccuracyRating++;
                            }
                            else
                            {
                                offersDetected.Add(offer, new Accuracy());
                            }
                        }
                    }
                }
            }

            _logger.LogDebug($"Potential offers detected: { offersDetected.Count }", offersDetected);

            // If no matches could be found, throw exception to notify the user
            if (offersDetected.Where(x => x.Value.HasMetMinimumCriteria == true).Count() == 0)
            {
                _logger.LogInformation("Failed to auto-match an offer with the analysed content from the image uploaded.", analysedContent);
                throw new Exception("Sorry, we could not find a matching offer.");
            }

            _logger.LogDebug($"Offers detected: { offersDetected.Count }", offersDetected);

            // Gets the most accurate offer based on if the minimum criteria has been met
            // and takes the highest accuracy rating offer
            OfferModel mostAccurateOffer = offersDetected
                .Where(x => x.Value.HasMetMinimumCriteria == true)
                .OrderByDescending(x => x.Value.AccuracyRating)
                .First().Key;

            // Callback to the delegate to run the SelectOffer(OfferModel offer) method on Rebates.razor
            selectOfferCallback(mostAccurateOffer);
        }

        /// <summary>
        ///     Retrieves all offers and brands from database.
        /// </summary>
        private async void GetAllOffers()
        {
            try
            {
                _allOffers = await _offerData.GetOffers();
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Initialising OfferFilter failed, error occured retrieving all offers.");
            }
        }
    }

    class AmountMatch
    {
        public int Amount { get; set; } = -1;
        public string AmountType { get; set; } = null;
    }

    class Accuracy
    {
        /// <summary>
        ///     The accuracy as a number representing the amount of matches between an offer/product/tag/offer details
        ///     and the analysed content. The higher the number, the more accurate it is.
        /// </summary>
        public int AccuracyRating { get; set; } = 0;

        /// <summary>
        ///     Minimum criteria required to enable auto selecting a rebate. The minimum criteria being the 
        ///     analysed product image content matches an offer's product name.
        ///     E.g. Criteria is true if the analysed content = "Interceptor" and the current iteration of the offer's product
        ///     is "Interceptor Plus".
        /// </summary>
        public bool HasMetMinimumCriteria { get; set; } = false;
    }
}
