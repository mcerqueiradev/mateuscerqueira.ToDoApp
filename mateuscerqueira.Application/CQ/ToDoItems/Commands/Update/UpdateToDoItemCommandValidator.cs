using FluentValidation;

namespace mateuscerqueira.Application.CQ.ToDoItems.Commands.Update;

public class UpdateToDoItemCommandValidator : AbstractValidator<UpdateToDoItemCommand>
{
    public UpdateToDoItemCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Id is required");

        RuleFor(x => x.Title)
            .NotEmpty()
            .WithMessage("Title is required")
            .MaximumLength(200)
            .WithMessage("Title must be at most 200 characters");

        RuleFor(x => x.Description)
            .MaximumLength(1000)
            .WithMessage("Description must be at most 1000 characters")
            .When(x => !string.IsNullOrEmpty(x.Description));

        RuleFor(x => x.UserId)
            .Must(userId => userId == null || userId != Guid.Empty)
            .WithMessage("User ID must be a valid GUID")
            .When(x => x.UserId.HasValue);
    }
}
