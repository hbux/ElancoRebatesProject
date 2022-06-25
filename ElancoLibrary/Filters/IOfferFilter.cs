using ElancoLibrary.Models.Offers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElancoLibrary.Filters
{
    public interface IOfferFilter
    {
        void AutoMatchOffer(List<string> analysedContent, Action<OfferModel> selectRebate);
    }
}
