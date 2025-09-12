using mateuscerqueira.Application.Common.Responses;
using MediatR;

namespace mateuscerqueira.Application.CQ.Auth.Commands;

public class LoginCommand : IRequest<Result<LoginResponse, Success, Error>>
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
