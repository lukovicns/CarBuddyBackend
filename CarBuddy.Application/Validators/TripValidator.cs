using CarBuddy.Domain.Extensions;
using CarBuddy.Domain.Models;
using FluentValidation;

namespace CarBuddy.Application.Validators
{
    public class TripValidator : AbstractValidator<Trip>
    {
        public TripValidator()
        {
            var requiredStringValidator = new RequiredStringValidator<Trip>();

            RuleFor(t => t.DriverId)
                .NotEmpty();

            RuleFor(u => u.FromCity)
                .SetValidator(requiredStringValidator);

            RuleFor(u => u.ToCity)
               .SetValidator(requiredStringValidator);

            RuleFor(u => u.Date)
                .SetValidator(new DateValidator<Trip>());

            RuleFor(u => u.StartTime)
                .NotEmpty();

            RuleFor(u => u.ArriveTime)
                .NotEmpty();

            RuleFor(u => u.Price)
                .NotEmpty()
                .InclusiveBetween(100, 10000);

            RuleFor(u => new { u.StartTime, u.ArriveTime })
                .Must(u => u.StartTime.IsLessThan(u.ArriveTime))
                .WithMessage("Start time must be greater than arrive time.");
        }
    }
}
