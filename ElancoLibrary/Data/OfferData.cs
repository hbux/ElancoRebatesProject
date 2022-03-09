using ElancoLibrary.DataAccess;
using ElancoLibrary.Models.Offers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElancoLibrary.Data
{
    public class OfferData
    {
        private ISqlDataAccess dataAccess;

        public OfferData(ISqlDataAccess dataAccess)
        {
            this.dataAccess = dataAccess;
        }

        /// <summary>
        ///     This method retrieves all offers/rebates within the ElancoData database.
        /// </summary>
        /// <returns>A complete list of all offers.</returns>
        public async Task<List<OfferModel>> GetOffers()
        {
            var offers = await dataAccess.LoadData<OfferModel, dynamic>("dbo.spOffer_GetAll", new { }, "ElancoData");

            foreach (OfferModel offer in offers)
            {
                var p = new
                {
                    offerId = offer.Id
                };

                var offerDetails = await dataAccess.LoadData<OfferDetails, dynamic>("dbo.spOfferDetails_GetById", p, "ElancoData");
                var offerProducts = await dataAccess.LoadData<ProductModel, dynamic>("dbo.spProduct_GetById", p, "ElancoData");

                offer.Details = offerDetails;
                offer.Products = offerProducts;
            }

            return offers;
        }
    }
}
