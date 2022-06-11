using Azure;
using Azure.AI.FormRecognizer;
using Azure.AI.FormRecognizer.Models;
using ElancoLibrary.Functions.Models;
using Microsoft.Azure.WebJobs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElancoLibrary.Functions.Services
{
    public class BlobService
    {
        [FunctionName("AnalyzeInvoice")]
        [return: Table("ImageText", Connection = "StorageConnection")]
        public async Task<ImageContent> Run([BlobTrigger("erp-invoice-container/{name}", Connection = "StorageConnection")] Stream myBlob, string name)
        {
            // Get connection configurations
            string subscriptionKey = Environment.GetEnvironmentVariable("FormRecognizerApiKey");
            string endpoint = Environment.GetEnvironmentVariable("FormRecognizerEndpoint");
            string imgUrl = $"https://{ Environment.GetEnvironmentVariable("StorageAccountName")}.blob.core.windows.net/erp-invoice-container/{name}";
            string modelId = Environment.GetEnvironmentVariable("FormRecognizerModelId");

            FormRecognizerClient client = new FormRecognizerClient(new Uri(endpoint), new AzureKeyCredential(subscriptionKey));

            string analyzedText = await AnalyzeUpload(client, imgUrl, modelId);

            return new ImageContent
            {
                PartitionKey = "Images",
                RowKey = Guid.NewGuid().ToString(),
                Text = analyzedText
            };
        }

        private async Task<string> AnalyzeUpload(FormRecognizerClient client, string imgUrl, string modelId)
        {
            RecognizedFormCollection invoices = await client.StartRecognizeCustomFormsFromUri(modelId, new Uri(imgUrl))
                .WaitForCompletionAsync();

            RecognizedForm invoice = invoices.Single();

            string text = "";

            foreach (KeyValuePair<string, FormField> kvp in invoice.Fields)
            {
                if (kvp.Value != null && kvp.Value.ValueData != null)
                {
                    try
                    {
                        text += kvp.Value.ValueData.Text;
                    }
                    catch (Exception)
                    {
                        continue;
                    }
                }
            }

            return text;
        }
    }
}
