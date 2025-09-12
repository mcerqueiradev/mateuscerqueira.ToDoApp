using FluentValidation;

namespace mateuscerqueira.Application.CQ.Users.Commands.Update;

public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserCommandValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty()
            .WithMessage("User ID is required");

        RuleFor(x => x.FirstName)
            .MaximumLength(50)
            .WithMessage("First name must be at most 50 characters")
            .When(x => !string.IsNullOrEmpty(x.FirstName));

        RuleFor(x => x.LastName)
            .MaximumLength(100)
            .WithMessage("Last name must be at most 100 characters")
            .When(x => !string.IsNullOrEmpty(x.LastName));

        RuleFor(x => x.Email)
            .EmailAddress()
            .WithMessage("Invalid email format")
            .When(x => !string.IsNullOrEmpty(x.Email));

        RuleFor(x => x.CurrentPassword)
            .NotEmpty()
            .WithMessage("Current password is required when changing password")
            .When(x => !string.IsNullOrEmpty(x.NewPassword));

        RuleFor(x => x.NewPassword)
            .MinimumLength(6)
            .WithMessage("New password must be at least 6 characters")
            .MaximumLength(100)
            .WithMessage("New password must be at most 100 characters")
            .When(x => !string.IsNullOrEmpty(x.NewPassword));

        RuleFor(x => x)
            .Must(x => !string.IsNullOrEmpty(x.NewPassword) ||
                       !string.IsNullOrEmpty(x.FirstName) ||
                       !string.IsNullOrEmpty(x.LastName) ||
                       !string.IsNullOrEmpty(x.Email) ||
                       x.IsActive.HasValue)
            .WithMessage("At least one field must be provided for update");
    }
}