using ElancoLibrary.Data;
using ElancoLibrary.Models;
using ElancoLibrary.Models.Brands;
using ElancoLibrary.Models.Offers;
using ElancoLibrary.Models.Products;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElancoLibrary.Helpers
{
    public class OfferHelper : IOfferHelper
    {
        private ILogger<OfferHelper> _logger;
        private IOfferData _offerData;
        private IBrandData _brandData;
        private List<OfferModel> _allOffers;
        private List<BrandModel> _allBrands;

        public OfferHelper(ILogger<OfferHelper> logger, IOfferData offerData, IBrandData productData)
        {
            _logger = logger;
            _offerData = offerData;
            _brandData = productData;

            GetAllOffersAndBrands();
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
            Dictionary<OfferModel, CriteriaModel> offerCriteriaAccuracy = new Dictionary<OfferModel, CriteriaModel>();

            _logger.LogDebug("AutoMatchOffer started at {Time}", DateTime.UtcNow);
            _logger.LogDebug("Amount of Text detected from product image analysis: {Count}", analysedContent.Count);

            // TODO?
            // - Create a list of keywords to validate e.g. dogs, cats, product names, number (450) followed by mg or ml
            // - Add pet type column into database table. OfferDetails? Product? New table?
            //   Credelio is the name of the product type, but a product should be for dogs, amount of tablets/ml/mg, productTypeId 
            // - Only 1 product name detected on image? Don't display any offers for two products
            // - Look at the '3 tablets' compare with offer details 3 tablets = 3 doses

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
                            if (offerCriteriaAccuracy.ContainsKey(offer) == true)
                            {
                                // Increase the accuracy rating if offer is inside dictionary
                                // Also sets met criteria to true
                                offerCriteriaAccuracy[offer].Accuracy++;
                                offerCriteriaAccuracy[offer].HasMetMinimumCriteria = true;
                            }
                            else
                            {
                                // Increase the accuracy rating if offer is not inside dictionary
                                // and sets minimum criteria to true via constructor
                                offerCriteriaAccuracy.Add(offer, new CriteriaModel(true));
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
                            if (offerCriteriaAccuracy.ContainsKey(offer) == true)
                            {
                                offerCriteriaAccuracy[offer].Accuracy++;
                            }
                            else
                            {
                                offerCriteriaAccuracy.Add(offer, new CriteriaModel(false));
                            }
                        }
                    }
                }
            }

            _logger.LogDebug("Potential offers detected: {Count}", offerCriteriaAccuracy.Count);

            // If no matches could be found, throw exception to notify the user
            if (offerCriteriaAccuracy.Where(x => x.Value.HasMetMinimumCriteria == true).Count() == 0)
            {
                _logger.LogInformation("AutoMatchOffer failed to match an offer with the product image uploaded.");
                throw new Exception("Sorry, we could not find a matching offer.");
            }

            // Gets the most accurate offer based on if the minimum criteria has been met
            // and takes the highest accuracy rating offer
            OfferModel mostAccurateOffer = offerCriteriaAccuracy
                .Where(x => x.Value.HasMetMinimumCriteria == true)
                .OrderByDescending(x => x.Value.Accuracy)
                .First().Key;

            _logger.LogDebug("AutoMatchOffer completed at {Time}", DateTime.UtcNow);

            // Callback to the delegate to run the SelectOffer(OfferModel offer) method on Rebates.razor
            selectOfferCallback(mostAccurateOffer);
        }

        public List<OfferModel> FilterOffers(string searchTerm)
        {
            // Search functionality on input or on button click?
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Retrieves all offers and products from database.
        /// </summary>
        private async void GetAllOffersAndBrands()
        {
            try
            {
                _allOffers = await _offerData.GetOffers();
                _allBrands = await _brandData.GetBrands();
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Error occured retrieving all offers and products.");
                throw;
            }
        }
    }
}
