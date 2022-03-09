using ElancoLibrary.Models.Offers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElancoLibrary.Data
{
    public interface IOfferData
    {
        Task<List<OfferModel>> GetOffers();
    }
}
