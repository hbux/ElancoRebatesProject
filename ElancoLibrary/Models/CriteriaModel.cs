using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElancoLibrary.Models
{
    public class CriteriaModel
    {
        public CriteriaModel(bool hasMetMinimumCriteria)
        {
            HasMetMinimumCriteria = hasMetMinimumCriteria;
        }

        /// <summary>
        ///     The accuracy as a number representing the amount of matches between an offer/product/tag/offer details
        ///     and the analysed content. The higher the number, the more accurate it is. Average accuracy = 3
        /// </summary>
        public int Accuracy { get; set; } = 1;

        /// <summary>
        ///     Minimum criteria required to enable auto selecting a rebate. The minimum criteria being the 
        ///     analysed product image content matches an offer's product name.
        ///     E.g. Criteria is true if the analysed content = "Interceptor" and the current iteration of the offer's product
        ///     is "Interceptor Plus".
        /// </summary>
        public bool HasMetMinimumCriteria { get; set; }
    }
}
