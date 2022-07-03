using Azure.Storage.Blobs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public async Task UploadInvoice(string filePath, string fileName)
        {
            using (FileStream stream = new FileStream(filePath, FileMode.Open))
            {
                BlobContainerClient containerClient = _serviceClient.GetBlobContainerClient("erp-invoice-container");
                BlobClient client = containerClient.GetBlobClient(fileName);

                await client.UploadAsync(stream);
            }

            _logger.LogDebug("Image uploaded to blob storage at {Time}", DateTime.UtcNow);
        }
    }
}
