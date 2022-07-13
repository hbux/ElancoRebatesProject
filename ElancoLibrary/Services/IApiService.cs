
namespace ElancoLibrary.Services
{
    public interface IApiService
    {
        Task<Dictionary<string, string>> AnalyseInvoice(string filePath);
        Task<List<string>> AnalyseProductImage(string filePath);
    }
}
