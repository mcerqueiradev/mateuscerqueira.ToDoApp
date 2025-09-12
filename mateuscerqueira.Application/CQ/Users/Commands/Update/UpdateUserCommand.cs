using mateuscerqueira.Application.Common.Responses;
using MediatR;

namespace mateuscerqueira.Application.CQ.Users.Commands.Update;

public class UpdateUserCommand : IRequest<Result<UpdateUserResponse, Success, Error>>
{
    public Guid UserId { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public string? CurrentPassword { get; set; }
    public string? NewPassword { get; set; }
    public bool? IsActive { get; set; }
}