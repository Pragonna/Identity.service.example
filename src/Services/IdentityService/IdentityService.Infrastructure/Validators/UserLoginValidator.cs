using FluentValidation;
using IdentityService.Infrastructure.Dtos;

namespace IdentityService.Infrastructure.Validators;

public class UserLoginValidator : AbstractValidator<UserLoginDto>
{
    public UserLoginValidator()
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
    }
}