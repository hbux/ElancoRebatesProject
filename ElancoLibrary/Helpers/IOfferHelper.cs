using ElancoLibrary.Models.Offers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElancoLibrary.Helpers
{
    public interface IOfferHelper
    {
        void AutoMatchOffer(List<string> analysedContent, Action<OfferModel> selectRebate);
        List<OfferModel> FilterOffers(string searchTerm);
    }
}
