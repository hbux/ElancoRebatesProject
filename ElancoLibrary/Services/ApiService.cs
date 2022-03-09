﻿using Azure.AI.FormRecognizer;
using Azure.AI.FormRecognizer.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElancoLibrary.Services
{
    public class ApiService
    {
        private IConfiguration _config;
        private FormRecognizerClient _client;

        public ApiService(IConfiguration config, FormRecognizerClient client)
        {
            _config = config;
            _client = client;
        }

        public async Task<Dictionary<string, string>> AnalyseInvoice(string filePath)
        {
            // Calls Azure Form Recognizer API to analyse the document
            // ModelId is the custom trained form recognizer AI id
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

        private Dictionary<string, string> ParseInvoice(RecognizedForm invoice)
        {
            Dictionary<string, string> fields = new Dictionary<string, string>();

            // Places the key and value into the fields dictionary
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
