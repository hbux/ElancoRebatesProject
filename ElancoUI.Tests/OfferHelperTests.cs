using ElancoLibrary.Models.Offers;
using ElancoUI.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ElancoUI.Tests
{
    public class OfferHelperTests
    {
        [Theory]
        [MemberData(nameof(GenerateCorrectContent))]
        public void FilterOffers_ShouldReturnFilteredListWithOffers(List<string> content)
        {
            // Initialising the required classes
            IOfferHelper offerHelper = new OfferHelper();

            // List<string> content is the analysed text from the users upload of their purchased product
            List<OfferModel> allOffers = GetSampleOffers();
            List<OfferModel> filteredOffers = offerHelper.FilterOffers(content, allOffers);

            // Testing that the filtered offers does contain values
            // TODO: Test that each value of content matches up with the filtered offers
            Assert.True(filteredOffers.Count > 0);
        }

        [Theory]
        [MemberData(nameof(GenerateIncorrectContent))]
        public void FilterOffers_ShouldReturnEmptyList(List<string> content)
        {
            // Initialising the required classes
            IOfferHelper offerHelper = new OfferHelper();

            // List<string> content is the analysed text from the users upload of their purchased product
            List<OfferModel> allOffers = GetSampleOffers();
            List<OfferModel> filteredOffers = offerHelper.FilterOffers(content, allOffers);

            // Testing that the filtered offers does not contain values
            Assert.True(filteredOffers.Count == 0);
        }

        private static IEnumerable<object[]> GenerateCorrectContent()
        {
            yield return new object[] { new List<string> { "Credelio" } };
            yield return new object[] { new List<string> { "Interceptor Plus", "Credelio" } };
        }

        private static IEnumerable<object[]> GenerateIncorrectContent()
        {
            yield return new object[] { new List<string> { "Cre" } };
            yield return new object[] { new List<string> { "Inteptor Pls", "lioCred" } };
        }

        private List<OfferModel> GetSampleOffers()
        {
            List<OfferModel> offers = new List<OfferModel>();

            // Creating offers with the minimum required data to test against (OfferCode & List of products)
            offers.Add(new OfferModel
            {
                OfferCode = "INCR22",
                Brands = new List<ProductTypeModel>
                {
                    new ProductTypeModel
                    {
                        Name = "Interceptor Plus"
                    },
                    new ProductTypeModel
                    {
                        Name = "Credelio"
                    }
                },
            });
            offers.Add(new OfferModel
            {
                OfferCode = "INAT22",
                Brands = new List<ProductTypeModel>
                {
                    new ProductTypeModel
                    {
                        Name = "Interceptor Plus"
                    },
                    new ProductTypeModel
                    {
                        Name = "Atopica"
                    }
                },
            });
            offers.Add(new OfferModel
            {
                OfferCode = "INT2022",
                Brands = new List<ProductTypeModel>
                {
                    new ProductTypeModel
                    {
                        Name = "Interceptor Plus"
                    }
                },
            });
            offers.Add(new OfferModel
            {
                OfferCode = "AT2022",
                Brands = new List<ProductTypeModel>
                {
                    new ProductTypeModel
                    {
                        Name = "Atopica"
                    }
                },
            });
            offers.Add(new OfferModel
            {
                OfferCode = "GPT2022",
                Brands = new List<ProductTypeModel>
                {
                    new ProductTypeModel
                    {
                        Name = "Galliprant"
                    }
                },
            });

            return offers;
        }
    }
}
