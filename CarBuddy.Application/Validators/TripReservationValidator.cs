using CarBuddy.Domain.Models;
using FluentValidation;

namespace CarBuddy.Application.Validators
{
    public class TripReservationValidator : AbstractValidator<TripReservation>
    {
        public TripReservationValidator()
        {
            RuleFor(tr => tr.UserId)
                .NotEmpty();

            RuleFor(tr => tr.NumberOfPassengers)
                .InclusiveBetween(1, 8)
                .NotEmpty();
        }
    }
}
