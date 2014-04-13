using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Model
{
    public static class ValidationExtensions
    {
        ///Denna metod har Mats Loock, LNU, skapat (eller alternativt använt sig av) i ett av sina exempel.
        public static bool Validate<T>(this T instance, out ICollection<ValidationResult> validationResults)
        {
            var validationContext = new ValidationContext(instance);
            validationResults = new List<ValidationResult>();
            return Validator.TryValidateObject(instance, validationContext, validationResults, true);
        }
    }
}