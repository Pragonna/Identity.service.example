using FluentValidation;
using IdentityService.Infrastructure.Dtos;

namespace IdentityService.Infrastructure.Validators;

public class UserRegisterValidator : AbstractValidator<UserRegisterDto>
{
    public UserRegisterValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress()
            .WithMessage("Please enter a valid email address.");

        RuleFor(x => x.Password)
            .NotEmpty()
            .MinimumLength(8)
            .MaximumLength(32)
            .WithMessage("Please enter a valid password.");

        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("Please enter a first name.");
        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Please enter a last name.");
    }
}