﻿using ElancoUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElancoUI.Helpers
{
    public class FormHelper
    {
        private Dictionary<string, string> states;   

        public FormHelper()
        {
            states = GenerateAllStates();
        }

        public FormModel FormatFields(Dictionary<string, string> fields)
        {
            FormModel _form = new FormModel();

            try
            {
                _form.ClinicName = fields["Name"];
                FormatAddress(_form, fields["Address"]);
            }
            catch (Exception)
            {
                return _form;
            }

            return _form;
        }

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
            // E.g. MI + 49660 

            // The format is always going to be STATE followed by ZIP CODE. If the addressStateZip count is
            // greater than 2, we loop over each one, if the states["Example"] suceeds, we assume the zip code
            // will follow.
            // If the states["Example"] fails, move on to the next index position and repeat.
            for (int i = 0; i < addressStateZip.Count; i++)
            {
                try
                {
                    form.ClinicState = states[addressStateZip[i]];
                    form.ClinicZipCode = addressStateZip[i + 1];

                    return;
                }
                catch (Exception)
                {
                    continue;
                }
            }
        }

        private Dictionary<string, string> GenerateAllStates()
        {
            // Easily access the state name from it's abbreviation e.g. TX returns Texas
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
    }
}
