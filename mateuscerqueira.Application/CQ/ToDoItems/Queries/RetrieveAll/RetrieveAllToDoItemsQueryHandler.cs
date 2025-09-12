using mateuscerqueira.Application.Common.Responses;
using mateuscerqueira.Application.CQ.ToDoItems.Queries.Retrieve;
using mateuscerqueira.ToDoApp.Domain.Core;
using mateuscerqueira.ToDoApp.Domain.Core.Uow;
using MediatR;

namespace mateuscerqueira.Application.CQ.ToDoItems.Queries.RetrieveAll;

public class RetrieveAllToDoItemsQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<RetrieveAllToDoItemsQuery, Result<PaginatedResult<RetrieveToDoItemResponse>, Success, Error>>
{
    public async Task<Result<PaginatedResult<RetrieveToDoItemResponse>, Success, Error>> Handle(
        RetrieveAllToDoItemsQuery request,
        CancellationToken cancellationToken)
    {
        var paginatedResult = await unitOfWork.ToDoItemRepository.GetPaginatedAsync(
            request.Page,
            request.PageSize,
            request.SearchTerm,
            request.IsCompleted,
            request.UserId,
            cancellationToken);

        var items = paginatedResult.Items.Select(toDoItem => new RetrieveToDoItemResponse
        {
            Id = toDoItem.Id,
            Title = toDoItem.Title,
            Description = toDoItem.Description,
            IsCompleted = toDoItem.IsCompleted,
            UserId = toDoItem.UserId,
            UserName = toDoItem.User?.Name.ToString(),
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