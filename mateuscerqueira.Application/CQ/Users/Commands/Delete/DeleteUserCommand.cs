using mateuscerqueira.Application.Common.Responses;
using MediatR;

namespace mateuscerqueira.Application.CQ.Users.Commands.Delete;

public class DeleteUserCommand : IRequest<Result<string, Success, Error>>
{
    public Guid UserId { get; set; }
}