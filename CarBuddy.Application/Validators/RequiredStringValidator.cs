using FluentValidation;
using FluentValidation.Validators;

namespace CarBuddy.Application.Validators
{
    public class RequiredStringValidator<T> : IPropertyValidator<T, string>
    {
        public string Name => "RequiredStringValidator";

        public string GetDefaultMessageTemplate(string errorCode)
            => "{PropertyName} is required.";

        public bool IsValid(ValidationContext<T> context, string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return false;

            value = value.Trim();

            if (value.Length < 2 || value.Length > 128)
            {
                context.AddFailure($"{context.PropertyName} must contain between 2 and 128 characters.");
                return false;
            }

            return true;
        }
    }
}
