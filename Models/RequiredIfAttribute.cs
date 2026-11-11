using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gharshare.Models
{
    [AttributeUsage(AttributeTargets.Property)]
    public class RequiredIfAttribute : ValidationAttribute
    {
        private readonly string _dependentPropertyName;
        private readonly object _dependentPropertyValue;

        //public bool HasAdditionalDetails { get; set; } // this will go in target class
        // where conditional validation is needed

       // [RequiredIf("HasAdditionalDetails", true, ErrorMessage = "Please enter your age")]
        // public int? Age { get; set; }
        // this is how conditional validation 
        //to be applied in target class.
        
        
        public RequiredIfAttribute(string dependentPropertyName, object dependentPropertyValue)
        {
            _dependentPropertyName = dependentPropertyName;
            _dependentPropertyValue = dependentPropertyValue;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var dependentProperty = validationContext.ObjectType.GetProperty(_dependentPropertyName);
            if (dependentProperty == null)
            {
                return new ValidationResult($"Unknown property {_dependentPropertyName}");
            }

            var dependentValue = dependentProperty.GetValue(validationContext.ObjectInstance, null);
            if (!Equals(dependentValue, _dependentPropertyValue))
            {
                return ValidationResult.Success;
            }

            if (value == null)
            {
                return new ValidationResult(ErrorMessage);
            }

            return ValidationResult.Success;
        }
    }

}
