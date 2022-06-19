using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElancoLibrary.Models.Offers
{
    public class TagModel
    {
        /// <summary>
        ///     The ID number of an individual product.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     The value of the tag. E.g. Interceptor, Plus, Advantage, Multi
        /// </summary>
        public string Value { get; set; }
    }
}
