using mateuscerqueira.Application.Common.Responses;
using MediatR;

namespace mateuscerqueira.Application.CQ.Users.Queries.Retrieve;

public class RetrieveUserQuery : IRequest<Result<RetrieveUserResponse, Success, Error>>
{
    public Guid Id { get; init; }
}
