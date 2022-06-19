﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElancoLibrary.Models.Offers
{
    public class OfferModel
    {
        /// <summary>
        ///     The ID number of an individual offer.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     The offer code of an offer. E.g. INCR22 
        /// </summary>
        public string OfferCode { get; set; }

        /// <summary>
        ///     The start of the offer date. Usually it is the 1st Jan of each year.
        /// </summary>
        public DateTime ValidPurchaseStart { get; set; }

        /// <summary>
        ///     The end of the offer date. Usually it is the 31st Dec of each year.
        /// </summary>
        public DateTime ValidPurchaseEnd { get; set; }

        /// <summary>
        ///     A list of products that the offer applies to.
        /// </summary>
        public List<ProductModel> Products { get; set; } = new List<ProductModel>();

        /// <summary>
        ///     A list of details regarding the offer. 
        /// </summary>
        public List<OfferDetailsModel> Details { get; set; } = new List<OfferDetailsModel>();

        /// <summary>
        ///     A string of extra information regarding the offer.
        /// </summary>
        public string AdditionalInformation { get; set; }
    }
}
