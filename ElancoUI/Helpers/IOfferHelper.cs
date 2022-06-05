using ElancoLibrary.Models.Offers;

namespace ElancoUI.Helpers
{
    public interface IOfferHelper
    {
        List<OfferModel> FilterOffers(List<string> content, List<OfferModel> offers);
    }
}