using Azure.AI.FormRecognizer;
using Azure.AI.FormRecognizer.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElancoLibrary.Services
{
    public class ApiService : IApiService
    {
        private IConfiguration _config;
        private FormRecognizerClient _client;

        public ApiService(IConfiguration config, FormRecognizerClient client)
        {
            _config = config;
            _client = client;
        }

        /// <summary>
        ///     Analyzes an uploaded file. This calls Azure Form Recognizer API which analyzes the document using
        ///     The model ID found in appsettings.json. The file path is the absolute source to the uploaded file. 
        ///     Depending on the environment either Development > unsafe_uploads OR Production > unsafe_uploads.
        /// </summary>
        /// <param name="filePath">The absolute source to the uploaded file.</param>
        /// <returns>A dictionary representing the key value pairs analysed from the uploaded file.</returns>
        public async Task<Dictionary<string, string>> AnalyseInvoice(string filePath)
        {
            using (FileStream stream = new FileStream(filePath, FileMode.Open))
            {
                RecognizedFormCollection invoices = await _client.StartRecognizeCustomForms(
                    _config["ModelId"],
                    stream)
                    .WaitForCompletionAsync();

                RecognizedForm invoice = invoices.Single();

                return ParseInvoice(invoice);
            }
        }

        /// <summary>
        ///     Places the key and value into a usable format.
        /// </summary>
        /// <param name="invoice">The single invoice returned from the API call.</param>
        /// <returns>A dictionary representing the key value pairs from the uploaded document.</returns>
        private Dictionary<string, string> ParseInvoice(RecognizedForm invoice)
        {
            Dictionary<string, string> fields = new Dictionary<string, string>();

            // Loops over each field the API has detected and tries to access it's key and value. If it succeeds 
            // the key and value are added to the dictionary.
            foreach (KeyValuePair<string, FormField> kvp in invoice.Fields)
            {
                if (kvp.Value != null && kvp.Value.ValueData != null)
                {
                    try
                    {
                        fields[kvp.Key] = kvp.Value.ValueData.Text;
                    }
                    catch (Exception)
                    {
                        continue;
                    }
                }
            }

            return fields;
        }
    }
}
