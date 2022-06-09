using ElancoUI.Helpers;
using ElancoUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ElancoUI.Tests
{
    public class FormHelperTests
    {
        [Theory]
        [InlineData("4003 Chippewa Manistee, MI 49960")]
        [InlineData("3910 Mount Kalulu Drive Los Angeles, CA 102910")]
        public void FormatFields_FormAddressShouldContainValues(string address)
        {
            // Initialising the required classes
            IFormHelper formHelper = new FormHelper();
            FormModel form = new FormModel();

            // Creating the mocked dictionary which simulates the returned values of an uploaded invoice
            Dictionary<string, string> fields = new Dictionary<string, string>();
            fields["Name"] = "Veterinary Hospital";
            fields["Address"] = address;

            // Running the method to parse the string address into AddressLine1, City, State and ZipCode
            formHelper.FormatFields(form, fields);

            // Testing that the form model contains values
            Assert.False(string.IsNullOrEmpty(form.ClinicAddress));
            Assert.False(string.IsNullOrEmpty(form.ClinicCity));
            Assert.False(string.IsNullOrEmpty(form.ClinicState));
            Assert.False(string.IsNullOrEmpty(form.ClinicZipCode));
        }

        [Fact]
        public void FormatFields_FormAddressShouldParseCorrectly()
        {
            // Address to test
            string address = "824 Fairmont Parkway Pasedena, TX 77504";

            // Initialising the required classes
            IFormHelper formHelper = new FormHelper();
            FormModel form = new FormModel();

            // Creating the mocked dictionary which simulates the returned values of an uploaded invoice
            Dictionary<string, string> fields = new Dictionary<string, string>();
            fields["Name"] = "Veterinary Hospital";
            fields["Address"] = address;

            // Running the method to parse the string address into AddressLine1, City, State and ZipCode
            formHelper.FormatFields(form, fields);

            // Expected results
            string expectedAddress = "824 Fairmont Parkway";
            string expectedCity = "Pasedena";
            string expectedState = "Texas";
            string expectedZipCode = "77504";

            // Actual results
            Assert.Equal(expectedAddress, form.ClinicAddress);
            Assert.Equal(expectedCity, form.ClinicCity);
            Assert.Equal(expectedState, form.ClinicState);
            Assert.Equal(expectedZipCode, form.ClinicZipCode);
        }
    }
}
