using mateuscerqueira.Application.Common.Responses;
using mateuscerqueira.Application.Security.Interfaces;
using mateuscerqueira.Application.Security.Services;
using mateuscerqueira.ToDoApp.Domain.Core.Uow;
using MediatR;

namespace mateuscerqueira.Application.CQ.Auth.Commands;

public class LoginCommandHandler(
    IUnitOfWork unitOfWork,
    PasswordService passwordService,
    ITokenService tokenService)
    : IRequestHandler<LoginCommand, Result<LoginResponse, Success, Error>>
{
    public async Task<Result<LoginResponse, Success, Error>> Handle(
        LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await unitOfWork.UserRepository.GetByEmailAsync(request.Email, cancellationToken);

        if (user == null || !user.IsActive)
        {
            return Error.InvalidCredentials;
        }

        if (!passwordService.VerifyPassword(user.Password, request.Password))
        {
            return Error.InvalidCredentials;
        }


        user.UpdateLastLogin();
        await unitOfWork.UserRepository.UpdateAsync(user);
        await unitOfWork.CommitAsync(cancellationToken);

        var token = tokenService.GenerateToken(
            user.Id,
            user.Name.FirstName,
            user.Name.LastName,
            user.Email.Value,
            user.Role.ToString());

        return new LoginResponse
        {
            Token = token,
            UserId = user.Id,
            FullName = user.Name.ToString(),
            Email = user.Email.Value,
            Role = user.Role.ToString(),
            ExpiresAt = DateTime.UtcNow.AddMinutes(tokenService.GetExpirationMinutes())
        };
    }
}