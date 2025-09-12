using mateuscerqueira.Application.Common.Responses;
using mateuscerqueira.Application.Security.Services;
using mateuscerqueira.ToDoApp.Domain.Core.Uow;
using mateuscerqueira.ToDoApp.Domain.Entities;
using mateuscerqueira.ToDoApp.Domain.ValueObjects;
using MediatR;

namespace mateuscerqueira.Application.CQ.Users.Commands.Update;

public class UpdateUserCommandHandler(IUnitOfWork unitOfWork, PasswordService passwordService)
    : IRequestHandler<UpdateUserCommand, Result<UpdateUserResponse, Success, Error>>
{
    public async Task<Result<UpdateUserResponse, Success, Error>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await unitOfWork.UserRepository.RetrieveAsync(request.UserId, cancellationToken);
        if (user == null)
        {
            return Error.UserNotFound;
        }

        if (!string.IsNullOrEmpty(request.Email) && request.Email != user.Email.Value)
        {
            var existingUser = await unitOfWork.UserRepository.GetByEmailAsync(request.Email, cancellationToken);
            if (existingUser != null)
            {
                return Error.ExistingUser;
            }
        }

        await UpdateUserInfo(user, request, cancellationToken);

        if (!string.IsNullOrEmpty(request.NewPassword))
        {
            var passwordUpdated = await UpdatePassword(user, request.CurrentPassword, request.NewPassword);
            if (!passwordUpdated)
            {
                return Error.InvalidPassword;
            }
        }

        if (request.IsActive.HasValue)
        {
            UpdateUserStatus(user, request.IsActive.Value);
        }

        await unitOfWork.UserRepository.UpdateAsync(user);
        await unitOfWork.CommitAsync(cancellationToken);

        return new UpdateUserResponse
        {
            UserId = user.Id,
            FullName = user.Name.ToString(),
            Email = user.Email.Value,
            Role = user.Role.ToString(),
            IsActive = user.IsActive,
            UpdatedAt = DateTime.UtcNow
        };
    }

    private async Task UpdateUserInfo(User user, UpdateUserCommand request, CancellationToken cancellationToken)
    {
        if (!string.IsNullOrEmpty(request.FirstName) || !string.IsNullOrEmpty(request.LastName))
        {
            var newName = new PersonalName(
                request.FirstName ?? user.Name.FirstName,
                request.LastName ?? user.Name.LastName
            );
            user.UpdatePersonalInfo(newName);
        }

        if (!string.IsNullOrEmpty(request.Email) && request.Email != user.Email.Value)
        {
            var newEmail = new Email(request.Email);
            // Você precisará implementar um método UpdateEmail na entidade User
            // user.UpdateEmail(newEmail);
        }
    }

    private async Task<bool> UpdatePassword(User user, string currentPassword, string newPassword)
    {
        if (!passwordService.VerifyPassword(user.Password, currentPassword))
        {
            return false;
        }

        var newPasswordHash = passwordService.CreatePasswordHash(newPassword);
        user.ChangePassword(newPasswordHash);

        return true;
    }

    private void UpdateUserStatus(User user, bool isActive)
    {
        if (isActive)
        {
            user.Activate();
        }
        else
        {
            user.Deactivate();
        }
    }
}