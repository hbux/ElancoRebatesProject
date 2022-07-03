using ElancoLibrary.DataAccess;
using ElancoLibrary.Models.Brands;
using ElancoLibrary.Models.Products;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElancoLibrary.Data
{
    public class BrandData : IBrandData
    {
        private readonly ISqlDataAccess _dataAccess;
        private readonly ILogger<BrandData> _logger;

        public BrandData(ISqlDataAccess dataAccess, ILogger<BrandData> logger)
        {
            _dataAccess = dataAccess;
            _logger = logger;
        }

        public async Task<List<BrandModel>> GetBrands()
        {
            var brands = await _dataAccess.LoadData<BrandModel, dynamic>("dbo.spBrand_GetAll", new { }, "ElancoData");

            if (brands == null)
            {
                _logger.LogWarning("Failed to retrieve all brands from database at {Time}", DateTime.UtcNow);
                throw new NullReferenceException("Failed to load brands.");
            }

            foreach (BrandModel brand in brands)
            {
                var p = new
                {
                    brandId = brand.Id
                };

                var rawProducts = await _dataAccess.LoadData<ProductModel, dynamic>("spProduct_GetById", p, "ElancoData");

                if (rawProducts == null)
                {
                    _logger.LogWarning("Failed to retrieve products of brand ID: {Id} from database at {Time}", brand.Id, DateTime.UtcNow);
                    throw new NullReferenceException($"Failed to load brand products by ID: { brand.Id }.");
                }

                brand.Products = rawProducts;
            }

            _logger.LogDebug("Retrieved all brands from database at {Time}", DateTime.UtcNow);

            return brands;
        }
    }
}
