using ElancoLibrary.Models.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElancoLibrary.Models.Brands
{
    public class BrandModel
    {
        /// <summary>
        ///     The ID number of an individual brand.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        ///     The name of the brand. E.g. Credelio
        /// </summary>
        public string BrandName { get; set; }

        /// <summary>
        ///     The absolute name of the image file found in wwwroot/storage/brands
        /// </summary>
        public string ImageName { get; set; }

        /// <summary>
        ///     The extension type of the image. E.g. .png, .jpg, .jpeg
        /// </summary>
        public string ImageType { get; set; }

        /// <summary>
        ///     A list of products the brand owns.
        /// </summary>
        public List<ProductModel> Products { get; set; } = new List<ProductModel>();
    }
}
