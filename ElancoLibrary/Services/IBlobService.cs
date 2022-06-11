
namespace ElancoLibrary.Services
{
    public interface IBlobService
    {
        Task UploadInvoice(string filePath);
    }
}