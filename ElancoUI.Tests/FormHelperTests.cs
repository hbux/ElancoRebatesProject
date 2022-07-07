using ElancoUI.Helpers;
using ElancoUI.Models;
using Microsoft.Extensions.Logging;
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
            // Initialising the required classes - use mocking to mock logger
            FormHelper formHelper = new FormHelper(null);
            FormDisplayModel form = new FormDisplayModel();

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

        [Theory]
        [InlineData("824 Fairmont Parkway Pasedena, TX 77504", "824 Fairmont Parkway", "Pasedena", "Texas", "77504")]
        [InlineData("1124 Myrte Rd. P.O Box E Walnutport, PA 18088", "1124 Myrte Rd. P.O Box E", "Walnutport", "Pennsylvania", "18088")]
        [InlineData("4006 Chippewa Manistee, MI 49660", "4006 Chippewa", "Manistee", "Michigan", "49660")]
        public void FormatFields_FormAddressShouldParseCorrectly(string address, string expectedAddressLine1, 
            string expectedCity, string expectedState, string expectedZipCode)
        {
            // Initialising the required classes - use mocking to mock logger
            FormHelper formHelper = new FormHelper(null);
            FormDisplayModel form = new FormDisplayModel();

            // Creating the mocked dictionary which simulates the returned values of an uploaded invoice
            Dictionary<string, string> fields = new Dictionary<string, string>();
            fields["Name"] = "Veterinary Hospital";
            fields["Address"] = address;

            // Running the method to parse the string address into AddressLine1, City, State and ZipCode
            formHelper.FormatFields(form, fields);

            // Expected results -> see test parameters
            // Actual results
            Assert.Equal(expectedAddressLine1, form.ClinicAddress);
            Assert.Equal(expectedCity, form.ClinicCity);
            Assert.Equal(expectedState, form.ClinicState);
            Assert.Equal(expectedZipCode, form.ClinicZipCode);
        }
    }
}
