using ElancoLibrary.DataAccess;
using ElancoLibrary.Models.Brands;
using ElancoLibrary.Models.Offers;
using ElancoLibrary.Models.Products;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElancoLibrary.Data
{
    public class OfferData : IOfferData
    {
        private ISqlDataAccess _dataAccess;
        private ILogger<OfferData> _logger;

        public OfferData(ISqlDataAccess dataAccess, ILogger<OfferData> logger)
        {
            _dataAccess = dataAccess;
            _logger = logger;
        }

        /// <summary>
        ///     This method retrieves all offers/rebates within the ElancoData database.
        /// </summary>
        /// <returns>A complete list of all offers.</returns>
        public async Task<List<OfferModel>> GetOffers()
        {
            var offers = await _dataAccess.LoadData<OfferModel, dynamic>("dbo.spOffer_GetAll", new { }, "ElancoData");

            if (offers == null)
            {
                _logger.LogWarning("Failed to retrieve all offers from database at {Time}", DateTime.UtcNow);
                throw new NullReferenceException("Failed to load available offers.");
            }

            foreach (OfferModel offer in offers)
            {
                var p = new
                {
                    offerId = offer.Id
                };

                var offerDetails = await _dataAccess.LoadData<OfferDetailsModel, dynamic>("dbo.spOfferDetails_GetById", p, "ElancoData");
                var offerBrands = await _dataAccess.LoadData<BrandModel, dynamic>("dbo.spBrand_GetById", p, "ElancoData");

                if (offerDetails == null)
                {
                    _logger.LogWarning("Failed to retrieve offer details of ID: {Id} from database at {Time}", offer.Id, DateTime.UtcNow);
                    throw new NullReferenceException($"Failed to load offer details by ID: { offer.Id }.");
                }
                if (offerBrands == null)
                {
                    _logger.LogWarning("Failed to retrieve offer brands of ID: {Id} from database at {Time}", offer.Id, DateTime.UtcNow);
                    throw new NullReferenceException($"Failed to load offer product types by ID: { offer.Id }.");
                }

                offer.Details = offerDetails;
                offer.Brands = offerBrands;
            }

            _logger.LogDebug("Retrieved all offers from database at {Time}", DateTime.UtcNow);

            return offers;
        }

        public async Task<OfferModel> GetOfferById(int offerId)
        {
            var p = new
            {
                OfferId = offerId
            };

            var rawOffer = await _dataAccess.LoadData<OfferModel, dynamic>("dbo.spOffer_GetById", p, "ElancoData");

            if (rawOffer == null)
            {
                _logger.LogWarning("Failed to retrieve offer by ID: {Id} from database at {Time}", offerId, DateTime.UtcNow);
                throw new NullReferenceException($"Failed to load offer by ID: { offerId }.");
            }

            OfferModel offer = rawOffer.FirstOrDefault();

            offer.Details = await _dataAccess.LoadData<OfferDetailsModel, dynamic>("dbo.spOfferDetails_GetById", p, "ElancoData");
            offer.Brands = await _dataAccess.LoadData<BrandModel, dynamic>("dbo.spBrand_GetById", p, "ElancoData");

            if (offer.Details == null)
            {
                _logger.LogWarning("Failed to retrieve offer details of ID: {Id} from database at {Time}", offer.Id, DateTime.UtcNow);
                throw new NullReferenceException($"Failed to load offer details by ID: { offerId }.");
            }
            if (offer.Brands == null)
            {
                _logger.LogWarning("Failed to retrieve offer brands of ID: {Id} from database at {Time}", offer.Id, DateTime.UtcNow);
                throw new NullReferenceException($"Failed to load offer product types by ID: { offerId }.");
            }

            _logger.LogDebug("Retrieved offer by ID: {Id} from database at {Time}", offerId ,DateTime.UtcNow);

            return offer;
        }
    }
}
