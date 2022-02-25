using System;
using System.Linq;
using Azure.AI.FormRecognizer;
using Azure.AI.FormRecognizer.Models;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace ElancoUI.Services
{
    public class FormRecogniserService
    {
        private IConfiguration _config;
        private FormRecognizerClient _client;

        public FormRecogniserService(IConfiguration config, FormRecognizerClient client)
        {
            _config = config;
            _client = client;
        }

        public async Task<RecognizedForm> AnalyseInvoice(string filePath)
        {
            RecognizeCustomFormsOperation operation = _client.StartRecognizeCustomFormsFromUri(
                _config["TrainedModelId"],
                new Uri(filePath));

            await operation.WaitForCompletionAsync();

            if (operation.HasValue == false)
            {
                throw new Exception("This document could not be recognized.");
            }

            return operation.Value.Single();
        }
    }
}
