using mateuscerqueira.Application.Common.Responses;
using mateuscerqueira.ToDoApp.Domain.Core.Uow;
using MediatR;

namespace mateuscerqueira.Application.CQ.ToDoItems.Commands.MarkAsComplete;

public class MarkToDoItemAsCompleteCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<MarkToDoItemAsCompleteCommand, Result<MarkToDoItemAsCompleteResponse, Success, Error>>
{
    public async Task<Result<MarkToDoItemAsCompleteResponse, Success, Error>> Handle(
        MarkToDoItemAsCompleteCommand request, CancellationToken cancellationToken)
    {
        var toDoItem = await unitOfWork.ToDoItemRepository.RetrieveAsync(request.Id, cancellationToken);

        if (toDoItem is null)
            return Error.ToDoItemNotFound;

        if (request.UserId.HasValue && toDoItem.UserId != request.UserId.Value)
            return Error.UnauthorizedAccess;

        if (toDoItem.IsCompleted)
        {
            toDoItem.MarkAsIncomplete();
        }
        else
        {
            toDoItem.MarkAsCompleted();
        }

        await unitOfWork.ToDoItemRepository.UpdateAsync(toDoItem);
        await unitOfWork.CommitAsync(cancellationToken);

        return new MarkToDoItemAsCompleteResponse
        {
            Id = toDoItem.Id,
            IsCompleted = toDoItem.IsCompleted,
            UpdatedAt = toDoItem.UpdatedAt ?? DateTime.UtcNow
        };
    }
}