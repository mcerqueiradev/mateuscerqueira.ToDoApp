using mateuscerqueira.Application.Common.Responses;
using mateuscerqueira.ToDoApp.Domain.Core.Uow;
using MediatR;

namespace mateuscerqueira.Application.CQ.ToDoItems.Queries.Retrieve;

public class RetrieveToDoItemQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<RetrieveToDoItemQuery, Result<RetrieveToDoItemResponse, Success, Error>>
{
    public async Task<Result<RetrieveToDoItemResponse, Success, Error>> Handle(
        RetrieveToDoItemQuery request,
        CancellationToken cancellationToken)
    {
        var toDoItem = await unitOfWork.ToDoItemRepository.RetrieveAsync(request.Id, cancellationToken);

        if (toDoItem == null)
        {
            return Error.ToDoItemNotFound;
        }

        return new RetrieveToDoItemResponse
        {
            Id = toDoItem.Id,
            Title = toDoItem.Title,
            Description = toDoItem.Description,
            IsCompleted = toDoItem.IsCompleted,
            UserId = toDoItem.UserId,
            UserName = toDoItem.User?.Name.ToString(),
            CreatedAt = toDoItem.CreatedAt,
            UpdatedAt = toDoItem.UpdatedAt
        };
    }
}