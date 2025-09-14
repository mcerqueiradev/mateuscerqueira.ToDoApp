using mateuscerqueira.Application.Common.Responses;
using mateuscerqueira.ToDoApp.Domain.Core.Uow;
using MediatR;

namespace mateuscerqueira.Application.CQ.Users.Queries.Retrieve;

public class RetrieveUserQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<RetrieveUserQuery, Result<RetrieveUserResponse, Success, Error>>
{
    public async Task<Result<RetrieveUserResponse, Success, Error>> Handle(
        RetrieveUserQuery request,
        CancellationToken cancellationToken)
    {
        var user = await unitOfWork.UserRepository.RetrieveAsync(request.Id, cancellationToken);

        if (user == null)
        {
            return Error.UserNotFound;
        }

        return new RetrieveUserResponse
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
        };
    }
}
