using Azure.AI.FormRecognizer;
using Azure.AI.FormRecognizer.Models;

namespace ElancoUI.Services
{
    public class FormRecognizerService
    {
        private IConfiguration _config;
        private FormRecognizerClient _client;

        public FormRecognizerService(IConfiguration config, FormRecognizerClient client)
        {
            _config = config;
            _client = client;
        }

        public async Task<RecognizedForm> AnalyseInvoice(string filePath)
        {
            RecognizeCustomFormsOperation documents = await _client.StartRecognizeCustomFormsFromUriAsync(
                _config["ModelId"],
                new Uri(filePath));

            RecognizedFormCollection invoices = documents.WaitForCompletion();

            if (invoices.Count > 1)
            {
                throw new Exception("A maximum of 1 document can be submitted.");
            }

            RecognizedForm invoice = invoices.Single();

            return invoice;
        }

        public void AnalyseInvoice(Stream stream)
        {
            throw new NotImplementedException();
        }
    }
}
