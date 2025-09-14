using mateuscerqueira.Application.Common.Responses;
using mateuscerqueira.Application.CQ.Users.Queries.Retrieve;
using mateuscerqueira.ToDoApp.Domain.Core;
using MediatR;

namespace mateuscerqueira.Application.CQ.Users.Queries.RetrieveAll;

public class RetrieveAllUsersQuery : IRequest<Result<PaginatedResult<RetrieveUserResponse>, Success, Error>>
{
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 20;
    public string? SearchTerm { get; set; }
    public bool? IsActive { get; set; }
}