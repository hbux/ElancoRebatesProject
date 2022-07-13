using ElancoLibrary.Models.Offers;

namespace ElancoLibrary.Data
{
    public interface IOfferData
    {
        Task<List<OfferModel>> GetOffers();
        Task<OfferModel> GetOfferById(int offerId);
    }
}
