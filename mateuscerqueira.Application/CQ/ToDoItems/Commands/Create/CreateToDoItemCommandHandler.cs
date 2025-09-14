using mateuscerqueira.Application.Common.Responses;
using mateuscerqueira.ToDoApp.Domain.Core.Uow;
using MediatR;

namespace mateuscerqueira.Application.CQ.ToDoItems.Commands.Create;

public class CreateToDoItemCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<CreateToDoItemCommand, Result<CreateToDoItemResponse, Success, Error>>
{
    public async Task<Result<CreateToDoItemResponse, Success, Error>> Handle(
        CreateToDoItemCommand request, CancellationToken cancellationToken)
    {
        if (request.UserId.HasValue)
        {
            var user = await unitOfWork.UserRepository.RetrieveAsync(request.UserId.Value, cancellationToken);
            if (user == null)
            {
                return Error.UserNotFound;
            }
        }

        var toDoItem = new ToDoApp.Domain.Entities.ToDoItem(request.Title, request.Description, request.UserId);

        await unitOfWork.ToDoItemRepository.AddAsync(toDoItem);
        await unitOfWork.CommitAsync(cancellationToken);


        return new CreateToDoItemResponse
        {
            Id = toDoItem.Id,
            Title = toDoItem.Title,
            Description = toDoItem.Description,
            IsCompleted = toDoItem.IsCompleted,
            UserId = toDoItem.UserId,
            CreatedAt = toDoItem.CreatedAt
        };
    }
}