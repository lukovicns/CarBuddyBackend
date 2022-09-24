using CarBuddy.Domain.Models;
using FluentValidation;

namespace CarBuddy.Application.Validators
{
    public class CredentialsValidator : AbstractValidator<Credentials>
    {
        public CredentialsValidator()
        {
            RuleFor(u => u.Email)
                .NotEmpty()
                .MinimumLength(2)
                .MaximumLength(128)
                .Matches("^([\\w\\.\\-]+)@([\\w\\-]+)((\\.(\\w){2,3})+)$");

            RuleFor(u => u.Password)
                .NotEmpty()
                .MinimumLength(2)
                .MaximumLength(128)
                .Matches("^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z]).{8,}$");
        }
    }
}
