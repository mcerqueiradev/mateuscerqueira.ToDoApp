using FluentValidation;

namespace mateuscerqueira.Application.CQ.Auth.Commands;

public class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("Email is required.")
            .EmailAddress()
            .WithMessage("Email must be a valid email address.")
            .MaximumLength(150)
            .WithMessage("Email must be at most 150 characters.");

        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage("Password is required.")
            .MinimumLength(6)
            .WithMessage("Password must be at least 6 characters.")
            .MaximumLength(100)
            .WithMessage("Password must be at most 100 characters.");
    }
}