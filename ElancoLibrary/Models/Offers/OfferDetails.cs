using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElancoLibrary.Models.Offers
{
    public class OfferDetails
    {
        /// <summary>
        ///     The ID number of an offer's detail.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     The amount of items the offer applies to. E.g. 6 doses of Credelio.
        /// </summary>
        public string Amount { get; set; }

        /// <summary>
        ///     The value of the offer/rebate. E.g. $25.
        /// </summary>
        public decimal Value { get; set; }
    }
}
