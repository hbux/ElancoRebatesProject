using Autofac.Extras.Moq;
using ElancoLibrary.Data;
using ElancoLibrary.DataAccess;
using ElancoLibrary.Models.Offers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ElancoLibrary.Tests
{
    public class OfferDataTests
    {
        [Fact]
        public async Task GetOffers_ValidCall()
        {
            using (AutoMock mock = AutoMock.GetLoose())
            {
                mock.Mock<ISqlDataAccess>()
                    .Setup(x => x.LoadData<OfferModel, dynamic>("dbo.spOffer_GetAll", new { }, "ElancoData"))
                    .Returns(Task.FromResult(GetSampleOffers()));

                OfferData offerData = mock.Create<OfferData>();

                List<OfferModel> expected = GetSampleOffers();
                List<OfferModel> actual = await offerData.GetOffers();

                Assert.True(actual != null);
                Assert.Equal(expected.Count, actual?.Count);
            }
        }

        private List<OfferModel> GetSampleOffers()
        {
            List<OfferModel> offers = new List<OfferModel>();

            // Creating offers with the minimum required data to test against (OfferCode & List of products)
            offers.Add(new OfferModel
            {
                OfferCode = "INCR22",
                Products = new List<ProductModel>
                {
                    new ProductModel
                    {
                        Name = "Interceptor Plus"
                    },
                    new ProductModel
                    {
                        Name = "Credelio"
                    }
                },
            });
            offers.Add(new OfferModel
            {
                OfferCode = "INAT22",
                Products = new List<ProductModel>
                {
                    new ProductModel
                    {
                        Name = "Interceptor Plus"
                    },
                    new ProductModel
                    {
                        Name = "Atopica"
                    }
                },
            });
            offers.Add(new OfferModel
            {
                OfferCode = "INT2022",
                Products = new List<ProductModel>
                {
                    new ProductModel
                    {
                        Name = "Interceptor Plus"
                    }
                },
            });
            offers.Add(new OfferModel
            {
                OfferCode = "AT2022",
                Products = new List<ProductModel>
                {
                    new ProductModel
                    {
                        Name = "Atopica"
                    }
                },
            });
            offers.Add(new OfferModel
            {
                OfferCode = "GPT2022",
                Products = new List<ProductModel>
                {
                    new ProductModel
                    {
                        Name = "Galliprant"
                    }
                },
            });

            return offers;
        }
    }
}
