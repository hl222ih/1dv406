///Denna metod har Mats Loock, LNU, skapat!

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Model
{

    public static class ValidationExtensions
    {
        public static bool Validate<T>(this T instance, out ICollection<ValidationResult> validationResults)
        {
            var validationContext = new ValidationContext(instance);
            validationResults = new List<ValidationResult>();
            return Validator.TryValidateObject(instance, validationContext, validationResults, true);
        }
    }
}