using Azure.AI.FormRecognizer;
using Azure.AI.FormRecognizer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElancoLibrary.Services
{
    public class FormRecogniserService
    {
        private FormRecognizerClient _client;

        public FormRecogniserService(FormRecognizerClient client)
        {
            _client = client;
        }

        public async Task<RecognizedFormCollection> AnalyseInvoice(string filePath)
        {
            var options = new RecognizeInvoicesOptions()
            {
                Locale = "en-US"
            };

            RecognizedFormCollection invoices = await _client.StartRecognizeInvoicesFromUriAsync(new Uri(filePath), options)
                .WaitForCompletionAsync();

            return invoices;
        }

        public Dictionary<string, string> MapInvoiceDetails(RecognizedForm invoice)
        {
            Dictionary<string, string> rebateDetails = new Dictionary<string, string>(10);

            // This will map the analysed result into a usable format

            return rebateDetails;
        }
    }
}
