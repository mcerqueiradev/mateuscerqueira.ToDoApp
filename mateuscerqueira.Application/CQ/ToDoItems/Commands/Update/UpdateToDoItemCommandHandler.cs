using mateuscerqueira.Application.Common.Responses;
using mateuscerqueira.ToDoApp.Domain.Core.Uow;
using MediatR;

namespace mateuscerqueira.Application.CQ.ToDoItems.Commands.Update;

public class UpdateToDoItemCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<UpdateToDoItemCommand, Result<UpdateToDoItemResponse, Success, Error>>
{
    public async Task<Result<UpdateToDoItemResponse, Success, Error>> Handle(
        UpdateToDoItemCommand request, CancellationToken cancellationToken)
    {
        var toDoItem = await unitOfWork.ToDoItemRepository.RetrieveAsync(request.Id, cancellationToken);
        if (toDoItem is null)
        {
            return Error.ToDoItemNotFound;
        }

        if (request.UserId.HasValue)
        {
            var user = await unitOfWork.UserRepository.RetrieveAsync(request.UserId.Value, cancellationToken);
            if (user is null)
            {
                return Error.UserNotFound;
            }

            toDoItem.AssignToUser(request.UserId.Value);
        }
        else
        {
            toDoItem.UnassignFromUser();
        }

        toDoItem.UpdateTitle(request.Title);
        toDoItem.UpdateDescription(request.Description);

        if (request.IsCompleted)
            toDoItem.MarkAsCompleted();
        else
            toDoItem.MarkAsIncomplete();

        await unitOfWork.ToDoItemRepository.UpdateAsync(toDoItem);
        await unitOfWork.CommitAsync(cancellationToken);

        return new UpdateToDoItemResponse
        {
            Id = toDoItem.Id,
            Title = toDoItem.Title,
            Description = toDoItem.Description,
            IsCompleted = toDoItem.IsCompleted,
            UserId = toDoItem.UserId,
            UpdatedAt = toDoItem.UpdatedAt
        };
    }
}
