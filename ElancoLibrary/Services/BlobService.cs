using Azure.Storage.Blobs;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElancoLibrary.Services
{
    public class BlobService : IBlobService
    {
        private BlobServiceClient _serviceClient;

        public BlobService(BlobServiceClient serviceClient)
        {
            _serviceClient = serviceClient;
        }

        public async Task UploadInvoice(string filePath, string fileName)
        {
            using (FileStream stream = new FileStream(filePath, FileMode.Open))
            {
                BlobContainerClient containerClient = _serviceClient.GetBlobContainerClient("erp-invoice-container");
                BlobClient client = containerClient.GetBlobClient(fileName);

                await client.UploadAsync(stream);
            }
        }
    }
}
