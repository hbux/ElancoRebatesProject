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

        public async Task<List<OfferModel>> GetOffers()
        {
            return await dataAccess.LoadData<OfferModel, dynamic>("dbo.spOffer_GetAll", new { }, "ElancoData");
        }
    }
}
