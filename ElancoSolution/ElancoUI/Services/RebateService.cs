using ElancoUI.Models.Rebates;

namespace ElancoUI.Services
{
    public class RebateService
    {
        private List<IRebateProductModel> _rebates;

        public RebateService()
        {
            _rebates = CreateRebates();
        }

        public List<IRebateProductModel> GetAvailableRebates()
        {
            return _rebates;
        }

        private List<IRebateProductModel> CreateRebates()
        {
            List<IRebateProductModel> rebates = new List<IRebateProductModel>();

            for (int i = 0; i < 10; i++)
            {
                IRebateProductModel rebate = new RebateProductModel
                {
                    Name = "Credelio, Interceptor Plus",
                    OfferCode = "INCR22",
                    Logos =
                    {
                        "/wwwroot/images/credelio.jpg",
                        "/wwwroot/images/InterceptorPlus.png"
                    },
                    ValidPurchasedStart = DateOnly.FromDateTime(new DateTime()),
                    ValidPurchasedEnd = DateOnly.FromDateTime(new DateTime()),
                    Offers =
                    {
                        new OfferModel()
                        {
                            Amount = "6 doses of each product",
                            Value = 6
                        },
                        new OfferModel()
                        {
                            Amount = "12 doses of each product",
                            Value = 15
                        }
                    },
                    AdditionalInformation = "Offer also valid for Interceptor® (milbemycin oxime) purchases."
                };

                rebates.Add(rebate);
            }

            return rebates;
        }
    }
}
