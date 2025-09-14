using mateuscerqueira.Application.Common.Responses;
using mateuscerqueira.Application.CQ.ToDoItems.Queries.Retrieve;
using mateuscerqueira.ToDoApp.Domain.Core;
using MediatR;

namespace mateuscerqueira.Application.CQ.ToDoItems.Queries.RetrieveAll;

public class RetrieveAllToDoItemsQuery : IRequest<Result<PaginatedResult<RetrieveToDoItemResponse>, Success, Error>>
{
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 20;
    public string? SearchTerm { get; set; }
    public bool? IsCompleted { get; set; }
    public Guid? UserId { get; set; }
}
