using Azure.AI.FormRecognizer;
using Azure.AI.FormRecognizer.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElancoLibrary.Services
{
    public class FormRecogniserService
    {
        private IConfiguration _config;
        private FormRecognizerClient _client;

        public FormRecogniserService(FormRecognizerClient client, IConfiguration config)
        {
            _client = client;
            _config = config;
        }

        public async Task<RecognizedForm> AnalyseInvoice(string path)
        {
            RecognizeCustomFormsOperation operation = await _client.StartRecognizeCustomFormsFromUriAsync(
                _config["ModelId"], new Uri(path));

            await operation.WaitForCompletionAsync();

            return operation.Value.Single();
        }

        public Dictionary<string, string> MapVeterinaryDetails(RecognizedForm document)
        {
            Dictionary<string, string> rebateDetails = new Dictionary<string, string>();

            foreach (KeyValuePair<string, FormField> kvp in document.Fields)
            {
                if (kvp.Value.ValueData != null)
                {
                    try
                    {
                        rebateDetails[kvp.Key] = kvp.Value.ValueData.Text;
                    }
                    catch (Exception)
                    {
                        continue;
                    }
                }
            }

            return rebateDetails;
        }
    }
}
