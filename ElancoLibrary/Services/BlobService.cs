using Azure.Storage.Blobs;
using Microsoft.Extensions.Logging;

namespace ElancoLibrary.Services
{
    public class BlobService : IBlobService
    {
        private readonly BlobServiceClient _serviceClient;
        private readonly ILogger<BlobService> _logger;

        public BlobService(BlobServiceClient serviceClient, ILogger<BlobService> logger)
        {
            _serviceClient = serviceClient;
            _logger = logger;
        }

        /// <summary>
        ///     Uploads an uploaded invoice to a container in Azure Blob storage.
        /// </summary>
        /// <param name="filePath">The absolute source to the uploaded file</param>
        /// <param name="fileName">The name of the file that is being uploaded</param>
        /// <returns></returns>
        public async Task UploadInvoice(string filePath, string fileName)
        {
            using (FileStream stream = new FileStream(filePath, FileMode.Open))
            {
                BlobContainerClient containerClient = _serviceClient.GetBlobContainerClient("erp-invoice-container");
                BlobClient client = containerClient.GetBlobClient(fileName);

                await client.UploadAsync(stream);
            }

            _logger.LogDebug("Image uploaded (file name: {FileName}) to blob storage at {Time}", fileName ,DateTime.UtcNow);
        }
    }
}
