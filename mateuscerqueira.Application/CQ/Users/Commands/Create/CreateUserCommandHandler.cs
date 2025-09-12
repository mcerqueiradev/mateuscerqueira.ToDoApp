using mateuscerqueira.Application.Common.Responses;
using mateuscerqueira.Application.Security.Services;
using mateuscerqueira.ToDoApp.Domain.Core.Uow;
using mateuscerqueira.ToDoApp.Domain.Entities;
using mateuscerqueira.ToDoApp.Domain.ValueObjects;
using MediatR;

namespace mateuscerqueira.Application.CQ.Users.Commands.Create;

public class CreateUserCommandHandler(IUnitOfWork unitOfWork, PasswordService passwordService) : IRequestHandler<CreateUserCommand, Result<CreateUserResponse, Success, Error>>
{
    public async Task<Result<CreateUserResponse, Success, Error>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var existingUser = await unitOfWork.UserRepository.GetByEmailAsync(request.Email, cancellationToken);
        if (existingUser != null)
        {
            return Error.ExistingUser;
        }

        var name = new PersonalName(request.FirstName, request.LastName);
        var email = new Email(request.Email);
        var passwordHash = passwordService.CreatePasswordHash(request.Password);

        var user = new User(name, email, passwordHash);

        await unitOfWork.UserRepository.AddAsync(user);
        await unitOfWork.CommitAsync(cancellationToken);

        return new CreateUserResponse
        {
            UserId = user.Id,
            FullName = user.Name.ToString(),
            Email = user.Email.Value,
            Role = user.Role.ToString(),
            CreatedAt = user.CreatedAt,
        };
    }
}
