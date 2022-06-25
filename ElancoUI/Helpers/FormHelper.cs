using ElancoLibrary.Models;
using ElancoUI.Models;
using ElancoUI.Models.DbContextModels;

namespace ElancoUI.Helpers
{
    /// <summary>
    ///     This helper class formats the analysed fields into a UI FormModel instance.
    /// </summary>
    public class FormHelper
    {
        private ILogger<FormHelper> _logger;
        private Dictionary<string, string> states;

        public FormHelper(ILogger<FormHelper> logger)
        {
            _logger = logger;
            states = GenerateAllStates();
        }

        /// <summary>
        ///     This maps out each field within the dictionary and applies it to the current form the user is filling out.
        /// </summary>
        /// <param name="formDisplay">The current form the user is filling out.</param>
        /// <param name="fields">The returned key value pairs from the API call.</param>
        public void FormatFields(FormDisplayModel formDisplay, Dictionary<string, string> fields)
        {
            try
            {
                formDisplay.ClinicName = fields["Name"];
                FormatAddress(formDisplay, fields["Address"]);

                _logger.LogDebug("Fields mapped into form model instance at {Time}", DateTime.UtcNow);
            }
            catch (Exception)
            {
                return;
            }
        }

        /// <summary>
        ///     Splits the address analyzed from the API into address line 1, city, state and Zip code.
        /// </summary>
        /// <param name="formDisplay">The current form the user is filling out.</param>
        /// <param name="address">The address key within the fields dictionary.</param>
        private void FormatAddress(FormDisplayModel formDisplay, string address)
        {
            // Original format of address: 4006 Chippewa Manistee, MI 49660

            List<string> addressParts = address.Split(',').ToList();
            // E.g. 4006 Chippewa Manistee + MI 49660

            if (addressParts.Count == 1)
            {
                formDisplay.ClinicAddress = addressParts[0];

                return;
            }

            List<string> addressCity = addressParts[0].Trim().Split().ToList();
            // E.g. 4006 + Chippewa + Manistee

            formDisplay.ClinicCity = addressCity[addressCity.Count - 1];

            string clinicAddress = "";
            addressCity.Where(x => x != formDisplay.ClinicCity).ToList().ForEach(x => clinicAddress += x + " ");
            formDisplay.ClinicAddress = clinicAddress.Trim();

            List<string> addressStateZip = addressParts[1].Split().ToList();
            // E.g. MI + 49660 . Phone

            // The format is always going to be STATE followed by ZIP CODE. If the addressStateZip count is
            // greater than 2, we loop over each one, if the states["Example"] suceeds, we assume the zip code
            // will follow.
            // If the states["Example"] fails, move on to the next index position and repeat.
            for (int i = 0; i < addressStateZip.Count; i++)
            {
                try
                {
                    formDisplay.ClinicState = states[addressStateZip[i].ToUpper()];
                    formDisplay.ClinicZipCode = addressStateZip[i + 1];

                    return;
                }
                catch (Exception)
                {
                    continue;
                }
            }
        }

        public void FormatAccountDetails(Account account, FormDisplayModel formDisplay, FormDisplayInteractionModel formDisplayInteraction)
        {
            formDisplay.CustomerFirstName = account.FirstName;
            formDisplay.CustomerLastName = account.LastName;
            formDisplay.CustomerEmailAddress = account.User.Email;
            formDisplay.CustomerPhone = account.User.PhoneNumber;

            Address defaultAddress = account.Addresses.FirstOrDefault(a => a.IsDefault == true);
            formDisplay.CustomerAddress = defaultAddress.AddressLine1;
            formDisplay.CustomerCity = defaultAddress.City;
            formDisplay.CustomerState = defaultAddress.State;
            formDisplay.CustomerZipCode = defaultAddress.ZipCode;

            formDisplayInteraction.Pets = account.Pets;

            _logger.LogDebug("Account details mapped into form model instance for user ID: {Id} at {Time}", account.User.Id, DateTime.UtcNow);
        }

        /// <summary>
        ///     Creates a dictionary of state appreviation and the state name to
        ///     easily access the state name from it's abbreviation. E.g. states["TX"] returns Texas.
        /// </summary>
        /// <returns>A dictionary of key: state abbreviation and value: state name.</returns>
        private Dictionary<string, string> GenerateAllStates()
        {
            // Easily access the state name from it's abbreviation e.g. states["TX"] returns Texas
            Dictionary<string, string> states = new Dictionary<string, string>();

            states.Add("AL", "Alabama"); states.Add("AK", "Alaska"); states.Add("AZ", "Arizona");
            states.Add("AR", "Arkansas"); states.Add("CA", "California"); states.Add("CO", "Colorado");
            states.Add("CT", "Connecticut"); states.Add("DE", "Delaware"); states.Add("DC", "District of Columbia");
            states.Add("FL", "Florida"); states.Add("GA", "Georgia"); states.Add("HI", "Hawaii");
            states.Add("ID", "Idaho"); states.Add("IL", "Illinois"); states.Add("IN", "Indiana");
            states.Add("IA", "Iowa"); states.Add("KS", "Kansas"); states.Add("KY", "Kentucky");
            states.Add("LA", "Louisiana"); states.Add("ME", "Maine"); states.Add("MD", "Maryland");
            states.Add("MA", "Massachusetts"); states.Add("MI", "Michigan"); states.Add("MN", "Minnesota");
            states.Add("MS", "Mississippi"); states.Add("MO", "Missouri"); states.Add("MT", "Montana");
            states.Add("NE", "Nebraska"); states.Add("NV", "Nevada"); states.Add("NH", "New Hampshire");
            states.Add("NJ", "New Jersey"); states.Add("NM", "New Mexico"); states.Add("NY", "New York");
            states.Add("NC", "North Carolina"); states.Add("ND", "North Dakota"); states.Add("OH", "Ohio");
            states.Add("OK", "Oklahoma"); states.Add("OR", "Oregon"); states.Add("PA", "Pennsylvania");
            states.Add("RI", "Rhode Island"); states.Add("SC", "South Carolina"); states.Add("SD", "South Dakota");
            states.Add("TN", "Tennessee"); states.Add("TX", "Texas"); states.Add("UT", "Utah");
            states.Add("VT", "Vermont"); states.Add("VA", "Virginia"); states.Add("WA", "Washington");
            states.Add("WV", "West Virginia"); states.Add("WI", "Wisconsin"); states.Add("WY", "Wyoming");

            return states;
        }

        public FormModel FormatFormForSubmission(FormDisplayModel formDisplay, 
            FormDisplayInteractionModel formDisplayInteraction, string userId)
        {
            FormModel form = new FormModel
            {
                Id = Guid.NewGuid().ToString(),
                OfferId = formDisplayInteraction.RebateSelected.Id,
                UserId = userId,
                InvoiceFileName = formDisplay.UploadedInvoiceFileName,
                CustomerFirstName = formDisplay.CustomerFirstName,
                CustomerLastName = formDisplay.CustomerLastName,
                CustomerEmail = formDisplay.CustomerEmailAddress,
                CustomerAddressLine1 = formDisplay.CustomerAddress,
                CustomerCity = formDisplay.CustomerCity,
                CustomerState = formDisplay.CustomerState,
                CustomerZipCode = formDisplay.CustomerZipCode,
                CustomerPhone = formDisplay.CustomerPhone,
                PetName = formDisplay.PetName,
                ClinicName = formDisplay.ClinicName,
                ClinicAddressLine1 = formDisplay.ClinicAddress,
                ClinicCity = formDisplay.ClinicCity,
                ClinicState = formDisplay.ClinicState,
                ClinicZipCode = formDisplay.ClinicZipCode,
                AmountPurchased = formDisplay.AmountPurchased
            };

            _logger.LogDebug("UI form model instance mapped into database form model for user ID: {Id} at {Time}", userId, DateTime.UtcNow);

            return form;
        }
    }
}
