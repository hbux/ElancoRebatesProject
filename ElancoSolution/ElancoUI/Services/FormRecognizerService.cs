using Azure.AI.FormRecognizer;
using Azure.AI.FormRecognizer.Models;
using ElancoUI.Models;
using System.Text;

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

        public async Task<FormModel> ParseDataFromInvoice(RecognizedForm invoice)
        {
            Dictionary<string, string> fields = new Dictionary<string, string>();
            Dictionary<string, string> states = GetStatesDetails();

            try
            {
                await Task.Run(() => invoice.Fields.
                    Where(x => x.Value.ValueData != null).ToList().
                    ForEach(x => fields[x.Key] = x.Value.ValueData.Text));
            }
            catch (Exception)
            {
                throw;
            }

            string address = string.Empty;
            string city = string.Empty;
            string shortState = string.Empty;
            string zip = string.Empty;

            try
            {
                List<string> rawAddressParts = fields["Address"].Split(',').ToList();

                List<string> firstPartAddress = rawAddressParts[0].Split().ToList();
                city = firstPartAddress[firstPartAddress.Count - 1];

                StringBuilder builder = new StringBuilder();
                foreach (string part in firstPartAddress)
                {
                    if (part != city)
                    {
                        builder.Append(part + " ");
                    }
                }

                address = builder.ToString();

                List<string> lastPartAddress = rawAddressParts[1].Trim().Split().ToList();
                shortState = states[lastPartAddress[0]];
                zip = lastPartAddress[1];

            }
            catch (Exception)
            {
                throw;
            }

            return new FormModel()
            {
                ClinicName = fields["Name"].Trim(),
                ClinicAddress = address.Trim(),
                ClinicCity = city.Trim(),
                ClinicState = shortState.Trim(),
                ClinicZipCode = zip.Trim(),
            };
        }

        private Dictionary<string, string> GetStatesDetails()
        {
            Dictionary<string, string> states = new Dictionary<string, string>();

            states.Add("AL", "Alabama");
            states.Add("AK", "Alaska");
            states.Add("AZ", "Arizona");
            states.Add("AR", "Arkansas");
            states.Add("CA", "California");
            states.Add("CO", "Colorado");
            states.Add("CT", "Connecticut");
            states.Add("DE", "Delaware");
            states.Add("DC", "District of Columbia");
            states.Add("FL", "Florida");
            states.Add("GA", "Georgia");
            states.Add("HI", "Hawaii");
            states.Add("ID", "Idaho");
            states.Add("IL", "Illinois");
            states.Add("IN", "Indiana");
            states.Add("IA", "Iowa");
            states.Add("KS", "Kansas");
            states.Add("KY", "Kentucky");
            states.Add("LA", "Louisiana");
            states.Add("ME", "Maine");
            states.Add("MD", "Maryland");
            states.Add("MA", "Massachusetts");
            states.Add("MI", "Michigan");
            states.Add("MN", "Minnesota");
            states.Add("MS", "Mississippi");
            states.Add("MO", "Missouri");
            states.Add("MT", "Montana");
            states.Add("NE", "Nebraska");
            states.Add("NV", "Nevada");
            states.Add("NH", "New Hampshire");
            states.Add("NJ", "New Jersey");
            states.Add("NM", "New Mexico");
            states.Add("NY", "New York");
            states.Add("NC", "North Carolina");
            states.Add("ND", "North Dakota");
            states.Add("OH", "Ohio");
            states.Add("OK", "Oklahoma");
            states.Add("OR", "Oregon");
            states.Add("PA", "Pennsylvania");
            states.Add("RI", "Rhode Island");
            states.Add("SC", "South Carolina");
            states.Add("SD", "South Dakota");
            states.Add("TN", "Tennessee");
            states.Add("TX", "Texas");
            states.Add("UT", "Utah");
            states.Add("VT", "Vermont");
            states.Add("VA", "Virginia");
            states.Add("WA", "Washington");
            states.Add("WV", "West Virginia");
            states.Add("WI", "Wisconsin");
            states.Add("WY", "Wyoming");

            return states;
        }
    }
}
