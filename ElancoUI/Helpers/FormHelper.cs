﻿using ElancoUI.Models;
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
        /// <param name="form">The current form the user is filling out.</param>
        /// <param name="fields">The returned key value pairs from the API call.</param>
        public void FormatFields(FormModel form, Dictionary<string, string> fields)
        {
            try
            {
                form.ClinicName = fields["Name"];
                FormatAddress(form, fields["Address"]);

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
        /// <param name="form">The current form the user is filling out.</param>
        /// <param name="address">The address key within the fields dictionary.</param>
        private void FormatAddress(FormModel form, string address)
        {
            // Original format of address: 4006 Chippewa Manistee, MI 49660

            List<string> addressParts = address.Split(',').ToList();
            // E.g. 4006 Chippewa Manistee + MI 49660

            if (addressParts.Count == 1)
            {
                form.ClinicAddress = addressParts[0];

                return;
            }

            List<string> addressCity = addressParts[0].Trim().Split().ToList();
            // E.g. 4006 + Chippewa + Manistee

            form.ClinicCity = addressCity[addressCity.Count - 1];

            string clinicAddress = "";
            addressCity.Where(x => x != form.ClinicCity).ToList().ForEach(x => clinicAddress += x + " ");
            form.ClinicAddress = clinicAddress.Trim();

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
                    form.ClinicState = states[addressStateZip[i].ToUpper()];
                    form.ClinicZipCode = addressStateZip[i + 1];

                    return;
                }
                catch (Exception)
                {
                    continue;
                }
            }
        }

        public void FormatAccountDetails(Account account, FormModel form, FormInteractionModel formInteraction)
        {
            form.CustomerFirstName = account.FirstName;
            form.CustomerLastName = account.LastName;
            form.CustomerEmailAddress = account.User.Email;
            form.CustomerPhone = account.User.PhoneNumber;

            Address defaultAddress = account.Addresses.FirstOrDefault(a => a.IsDefault == true);
            form.CustomerAddress = defaultAddress.AddressLine1;
            form.CustomerCity = defaultAddress.City;
            form.CustomerState = defaultAddress.State;
            form.CustomerZipCode = defaultAddress.ZipCode;

            formInteraction.Pets = account.Pets;

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

        public ElancoLibrary.Models.FormModel FormatFormForSubmission(FormModel form, 
            FormInteractionModel formInteraction, string userId)
        {
            ElancoLibrary.Models.FormModel dbForm = new ElancoLibrary.Models.FormModel
            {
                Id = Guid.NewGuid().ToString(),
                OfferId = formInteraction.RebateSelected.Id,
                UserId = userId,
                InvoiceFileName = form.UploadedInvoiceFileName,
                CustomerFirstName = form.CustomerFirstName,
                CustomerLastName = form.CustomerLastName,
                CustomerEmail = form.CustomerEmailAddress,
                CustomerAddressLine1 = form.CustomerAddress,
                CustomerCity = form.CustomerCity,
                CustomerState = form.CustomerState,
                CustomerZipCode = form.CustomerZipCode,
                CustomerPhone = form.CustomerPhone,
                PetName = form.PetName,
                ClinicName = form.ClinicName,
                ClinicAddressLine1 = form.ClinicAddress,
                ClinicCity = form.ClinicCity,
                ClinicState = form.ClinicState,
                ClinicZipCode = form.ClinicZipCode,
                AmountPurchased = form.AmountPurchased
            };

            _logger.LogDebug("UI form model instance mapped into database form model for user ID: {Id} at {Time}", userId, DateTime.UtcNow);

            return dbForm;
        }
    }
}
