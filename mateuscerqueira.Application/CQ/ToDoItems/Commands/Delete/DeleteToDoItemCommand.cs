using mateuscerqueira.Application.Common.Responses;
using MediatR;

namespace mateuscerqueira.Application.CQ.ToDoItems.Commands.Delete;

public class DeleteToDoItemCommand : IRequest<Result<string, Success, Error>>
{
    public Guid Id { get; set; }
}
