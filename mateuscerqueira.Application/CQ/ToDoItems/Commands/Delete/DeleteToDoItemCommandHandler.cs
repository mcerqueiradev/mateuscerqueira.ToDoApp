using mateuscerqueira.Application.Common.Responses;
using mateuscerqueira.ToDoApp.Domain.Core.Uow;
using MediatR;

namespace mateuscerqueira.Application.CQ.ToDoItems.Commands.Delete;

public class DeleteToDoItemCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<DeleteToDoItemCommand, Result<string, Success, Error>>
{
    public async Task<Result<string, Success, Error>> Handle(
       DeleteToDoItemCommand request, CancellationToken cancellationToken)
    {
        var task = await unitOfWork.ToDoItemRepository.RetrieveAsync(request.Id, cancellationToken);

        if (task == null)
        {
            return Error.ToDoItemNotFound;
        }

        await unitOfWork.ToDoItemRepository.DeleteByIdAsync(task);
        await unitOfWork.CommitAsync(cancellationToken);

        return "Task deleted successfully";
    }
}
