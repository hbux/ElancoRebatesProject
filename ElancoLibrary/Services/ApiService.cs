using Azure.AI.FormRecognizer;
using Azure.AI.FormRecognizer.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElancoLibrary.Services
{
    public class ApiService : IApiService
    {
        private readonly IConfiguration _config;
        private readonly FormRecognizerClient _client;
        private readonly ILogger<ApiService> _logger;

        public ApiService(IConfiguration config, FormRecognizerClient client, ILogger<ApiService> logger)
        {
            _config = config;
            _client = client;
            _logger = logger;
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

                _logger.LogDebug("Analysed invoice for file path: {FilePath} at {Time}", filePath ,DateTime.UtcNow);

                return ParseInvoice(invoice, filePath);
            }
        }

        public async Task<List<string>> AnalyseProductImage(string filePath)
        {
            using (FileStream stream = new FileStream(filePath, FileMode.Open))
            {
                var rawUpload = await _client.StartRecognizeContent(stream)
                    .WaitForCompletionAsync();

                FormPage uploadedImage = rawUpload.Value.FirstOrDefault();

                _logger.LogDebug("Analysed product image for file path: {FilePath} at {Time}", filePath ,DateTime.UtcNow);

                return ParseProductImageContent(uploadedImage, filePath);
            }
        }

        /// <summary>
        ///     Places the key and value into a usable format.
        /// </summary>
        /// <param name="invoice">The single invoice returned from the API call.</param>
        /// <returns>A dictionary representing the key value pairs from the uploaded document.</returns>
        private Dictionary<string, string> ParseInvoice(RecognizedForm invoice, string filePath)
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

            _logger.LogDebug("Parsed invoice for file path: {filepath}, data collected: {Data}", filePath, fields);

            return fields;
        }

        private List<string> ParseProductImageContent(FormPage uploadedImage, string filePath)
        {
            List<string> parsedContent = new List<string>();

            foreach (FormLine text in uploadedImage.Lines)
            {
                parsedContent.Add(text.Text);
            }

            _logger.LogDebug("Parsed product image for file path: {filepath}, data collected: {Data}", filePath, parsedContent);

            return parsedContent;
        }
    }
}
