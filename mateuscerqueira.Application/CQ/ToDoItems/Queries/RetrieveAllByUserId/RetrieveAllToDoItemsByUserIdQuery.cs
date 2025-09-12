using mateuscerqueira.Application.Common.Responses;
using mateuscerqueira.Application.CQ.ToDoItems.Queries.Retrieve;
using mateuscerqueira.ToDoApp.Domain.Core;
using MediatR;

namespace mateuscerqueira.Application.CQ.ToDoItems.Queries.RetrieveAllByUserId;

public class RetrieveAllToDoItemsByUserIdQuery : IRequest<Result<PaginatedResult<RetrieveToDoItemResponse>, Success, Error>>
{
    public Guid UserId { get; set; }
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 20;
    public string? SearchTerm { get; set; }
    public bool? IsCompleted { get; set; }
}
