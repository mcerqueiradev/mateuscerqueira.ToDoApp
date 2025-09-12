using mateuscerqueira.Application.Common.Responses;
using mateuscerqueira.Application.CQ.Users.Queries.Retrieve;
using mateuscerqueira.ToDoApp.Domain.Core;
using mateuscerqueira.ToDoApp.Domain.Core.Uow;
using MediatR;

namespace mateuscerqueira.Application.CQ.Users.Queries.RetrieveAll;

public class RetrieveAllUsersQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<RetrieveAllUsersQuery, Result<PaginatedResult<RetrieveUserResponse>, Success, Error>>
{
    public async Task<Result<PaginatedResult<RetrieveUserResponse>, Success, Error>> Handle(
        RetrieveAllUsersQuery request,
        CancellationToken cancellationToken)
    {
        var paginatedResult = await unitOfWork.UserRepository.GetPaginatedAsync(
            request.Page,
            request.PageSize,
            request.SearchTerm,
            request.IsActive,
            cancellationToken);

        var items = paginatedResult.Items.Select(user => new RetrieveUserResponse
        {
            Id = user.Id,
            FirstName = user.Name.FirstName,
            LastName = user.Name.LastName,
            FullName = user.Name.ToString(),
            Email = user.Email.Value,
            Role = user.Role,
            IsActive = user.IsActive,
            LastLoginDate = user.LastLoginDate,
            CreatedAt = user.CreatedAt,
            UpdatedAt = user.UpdatedAt,
            ToDoItemsCount = user.ToDoItems?.Count ?? 0
        }).ToList();

        var response = new PaginatedResult<RetrieveUserResponse>(
            items,
            paginatedResult.TotalCount,
            paginatedResult.PageIndex,
            paginatedResult.PageSize);

        return response;
    }
}