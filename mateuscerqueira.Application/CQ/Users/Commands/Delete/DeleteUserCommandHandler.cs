using mateuscerqueira.Application.Common.Responses;
using mateuscerqueira.ToDoApp.Domain.Core.Uow;
using MediatR;

namespace mateuscerqueira.Application.CQ.Users.Commands.Delete;

public class DeleteUserCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<DeleteUserCommand, Result<string, Success, Error>>
{
    public async Task<Result<string, Success, Error>> Handle(
        DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var user = await unitOfWork.UserRepository.RetrieveAsync(request.UserId, cancellationToken);

        if (user == null)
        {
            return Error.UserNotFound;
        }

        if (user.ToDoItems?.Any() == true)
        {
            return Error.UserHasAssociatedTasks;
        }

        await unitOfWork.UserRepository.DeleteByIdAsync(user);
        await unitOfWork.CommitAsync(cancellationToken);

        return "User deleted successfully";
    }
}