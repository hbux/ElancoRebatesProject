using ElancoLibrary.Models.Brands;
using ElancoLibrary.Models.Products;

namespace ElancoLibrary.Data
{
    public interface IBrandData
    {
        Task<List<BrandModel>> GetBrands();
    }
}