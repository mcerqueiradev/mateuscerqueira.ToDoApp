using mateuscerqueira.Application.Common.Responses;
using MediatR;

namespace mateuscerqueira.Application.CQ.ToDoItems.Commands.MarkAsComplete;

public class MarkToDoItemAsCompleteCommand : IRequest<Result<MarkToDoItemAsCompleteResponse, Success, Error>>
{
    public Guid Id { get; set; }
    public Guid? UserId { get; set; }
}
