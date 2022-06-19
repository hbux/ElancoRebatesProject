using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElancoLibrary.Models.Offers
{
    public class ProductModel
    {
        /// <summary>
        ///     The ID number of an individual product.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        ///     The name of the product. E.g. Credelio
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     The absolute name of the image file found in wwwroot/storage/products
        /// </summary>
        public string ImageName { get; set; }

        /// <summary>
        ///     The extension type of the image. E.g. .png, .jpg, .jpeg
        /// </summary>
        public string ImageType { get; set; }

        /// <summary>
        ///     A list of tags associated with the product to allow for easier filtering/searching.
        /// </summary>
        public List<TagModel> Tags { get; set; } = new List<TagModel>();
    }
}
