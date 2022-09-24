using System;
using System.ComponentModel.DataAnnotations;

namespace CarBuddy.Application.Attributes
{
    public class GuidRequiredAttribute : RequiredAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (!Guid.TryParse(value.ToString(), out Guid valueGuid))
                return new ValidationResult("Invalid Guid provided.");

            if (valueGuid == Guid.Empty)
                return new ValidationResult("Value is required.");

            return ValidationResult.Success;
        }
    }
}
