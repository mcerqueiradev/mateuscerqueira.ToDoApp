using mateuscerqueira.Application.Common.Responses;
using mateuscerqueira.Application.CQ.ToDoItems.Queries.Retrieve;
using mateuscerqueira.ToDoApp.Domain.Core;
using mateuscerqueira.ToDoApp.Domain.Core.Uow;
using MediatR;

namespace mateuscerqueira.Application.CQ.ToDoItems.Queries.RetrieveAllByUserId;

public class RetrieveAllToDoItemsByUserIdQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<RetrieveAllToDoItemsByUserIdQuery, Result<PaginatedResult<RetrieveToDoItemResponse>, Success, Error>>
{
    public async Task<Result<PaginatedResult<RetrieveToDoItemResponse>, Success, Error>> Handle(
        RetrieveAllToDoItemsByUserIdQuery request,
        CancellationToken cancellationToken)
    {
        var user = await unitOfWork.UserRepository.RetrieveAsync(request.UserId, cancellationToken);
        if (user == null)
        {
            return Error.UserNotFound;
        }

        var paginatedResult = await unitOfWork.ToDoItemRepository.GetByUserIdAsync(
            request.UserId,
            request.Page,
            request.PageSize,
            request.SearchTerm,
            request.IsCompleted,
            cancellationToken);

        var items = paginatedResult.Items.Select(toDoItem => new RetrieveToDoItemResponse
        {
            Id = toDoItem.Id,
            Title = toDoItem.Title,
            Description = toDoItem.Description,
            IsCompleted = toDoItem.IsCompleted,
            UserId = toDoItem.UserId,
            UserName = user.Name.ToString(),
            CreatedAt = toDoItem.CreatedAt,
            UpdatedAt = toDoItem.UpdatedAt
        }).ToList();

        var response = new PaginatedResult<RetrieveToDoItemResponse>(
            items,
            paginatedResult.TotalCount,
            paginatedResult.PageIndex,
            paginatedResult.PageSize);

        return response;
    }
}