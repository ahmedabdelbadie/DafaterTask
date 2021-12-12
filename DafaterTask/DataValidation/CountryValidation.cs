using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace DafaterTask.DataValidation
{
    public class CountryValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null) return new ValidationResult("The Country Musn't Be Empty");
            var error = ValidateCountryFromExternalAsync(value.ToString()).Result;
            if (error == "False")
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult("The Country Is NOT Valid");
            }

        }
        public static async Task<string> ValidateCountryFromExternalAsync(string country)
        {
            string error = "True";
            using (var client = new HttpClient())
            {

                var postTask = client.PostAsync("https://countriesnow.space/api/v0.1/countries/flag/images", new FormUrlEncodedContent(new Dictionary<string, string> { { "country", country } }));
                postTask.Wait();
                var result = postTask.Result;
                if (result.Content is object && result.Content.Headers.ContentType.MediaType == "application/json")
                {
                    var jsonStr = await result.Content.ReadAsStringAsync();
                    JsonSerializer serializer = new JsonSerializer();
                    try
                    {
                        dynamic output = JObject.Parse(jsonStr);
                        error = output.error;
                    }
                    catch (JsonReaderException)
                    {
                        Console.WriteLine("Invalid JSON.");
                    }
                }
                return error;

            }

        }
    }


}