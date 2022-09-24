using CarBuddy.Domain.Models;
using FluentValidation;
using System;

namespace CarBuddy.Application.Validators
{
    public class CarValidator : AbstractValidator<Car>
    {
        public CarValidator()
        {
            var requiredStringValidator = new RequiredStringValidator<Car>();

            RuleFor(c => c.DriverId)
                .NotEmpty();

            RuleFor(c => c.Brand)
                .SetValidator(requiredStringValidator);

            RuleFor(c => c.Model)
                .SetValidator(requiredStringValidator);

            RuleFor(c => c.Photo)
                .MaximumLength(512);

            RuleFor(c => c.NumberOfSeats)
                .InclusiveBetween(1, 8)
                .NotEmpty();

            RuleFor(c => c.Year)
                .InclusiveBetween(1920, DateTime.Now.Year)
                .NotEmpty();
        }
    }
}
