using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElancoLibrary.Models.Products
{
    public class ProductModel
    {
        /// <summary>
        ///     The ID number of an individual product.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     The name of the product. E.g. Chewable tablets
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     The amount of the product. E.g. 3 (chewable tablets)
        /// </summary>
        public int Amount { get; set; }

        /// <summary>
        ///     The type of the amount. E.g. tablets
        /// </summary>
        public string AmountType { get; set; }

        /// <summary>
        ///     The size of the amount. E.g. 450 (mg)
        /// </summary>
        public int SizeOfAmount { get; set; }

        /// <summary>
        ///     The type of the amount size. E.g. mg
        /// </summary>
        public string SizeOfAmountType { get; set; }

        /// <summary>
        ///     The pet type the product is for. E.g. Dogs
        /// </summary>
        public string PetType { get; set; }

        /// <summary>
        ///     The minimum size of the pet the product is for. E.g. (between) 11-22
        /// </summary>
        public decimal PetTypeSizeMinimum { get; set; }

        /// <summary>
        ///     The maximum size of the pet the product is for. E.g. (between) 11-22
        /// </summary>
        public decimal PetTypeSizeMaximum { get; set; }

        /// <summary>
        ///     The size type of the pet the product is for. E.g. Kg
        /// </summary>
        public string SizeType { get; set; }
    }
}
