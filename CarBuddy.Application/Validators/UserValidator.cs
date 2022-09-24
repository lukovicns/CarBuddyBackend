using CarBuddy.Domain.Models;
using FluentValidation;

namespace CarBuddy.Application.Validators
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            var requiredStringValidator = new RequiredStringValidator<User>();

            RuleFor(u => u.FirstName)
                .MinimumLength(2)
                .SetValidator(requiredStringValidator);

            RuleFor(u => u.LastName)
                .MinimumLength(2)
                .SetValidator(requiredStringValidator);

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

            RuleFor(u => u.Age)
                .NotEmpty()
                .InclusiveBetween(10, 150);

            RuleFor(u => u.Photo)
               .MinimumLength(2)
               .MaximumLength(512);
        }
    }
}
