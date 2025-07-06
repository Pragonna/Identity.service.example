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
            .WithMessage("Password must be between 8 and 32 characters.")
            .Matches(@"[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
            .Matches(@"[0-9]").WithMessage("Password must contain at least one digit.")
            .Matches(@"[+\-@!#$%^&*(),.?""{}|<>]").WithMessage("Password must contain at least one special character.");

        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("Please enter a first name.");
        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Please enter a last name.");
    }
}