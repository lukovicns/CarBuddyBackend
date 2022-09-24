using CarBuddy.Domain.Extensions;
using FluentValidation;
using FluentValidation.Validators;
using System;

namespace CarBuddy.Application.Validators
{
    public class DateValidator<T> : IPropertyValidator<T, DateTime>
    {
        public string Name => "DateValidator";

        public string GetDefaultMessageTemplate(string errorCode)
            => "{PropertyName} is required.";

        public bool IsValid(ValidationContext<T> context, DateTime value)
        {
            if (value == null)
                return false;

            if (value.IsLessThan(DateTime.Now))
            {
                context.AddFailure($"{context.PropertyName} cannot be less than today.");
                return false;
            }

            if (value.IsGreaterThan(DateTime.Now.AddDays(13)))
            {
                context.AddFailure($"{context.PropertyName} cannot be more than 14 days.");
                return false;
            }

            return true;
        }
    }
}
