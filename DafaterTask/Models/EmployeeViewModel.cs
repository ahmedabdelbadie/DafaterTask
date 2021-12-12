using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;
using DafaterTask.DataValidation;

namespace DafaterTask.Models
{
    public class EmployeeViewModel : IValidatableObject
    {
        
        public long ID { get; set; }


        
        [Display(Name = "Name")]
        public string name { get; set; }


        [StringLength(50, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 5)]
        [Display(Name = "FamilyName")]
        public string familyName { get; set; }



        [StringLength(50, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 10)]
        [Display(Name = "Address")]
        public string address { get; set; }



        [Display(Name = "CountryOfOrigin")]
        [CountryValidation]
        public string countryOfOrigin { get; set; }



        [DataType(DataType.EmailAddress)]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 5)]
        [Display(Name = "EMailAdress")]
        public string email { get; set; }


        [Display(Name = "Age")]
        public int? age { get; set; }


        [Required]
        [Display(Name = "Hired")]
        public bool? hired { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (age < 20 || age > 60)
            {
                yield return new ValidationResult("Age Must Be Between 20 and 60");
            }
        }
    }
}