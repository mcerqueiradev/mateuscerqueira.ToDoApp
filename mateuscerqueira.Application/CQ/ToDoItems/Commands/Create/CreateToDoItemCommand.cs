using mateuscerqueira.Application.Common.Responses;
using MediatR;

namespace mateuscerqueira.Application.CQ.ToDoItems.Commands.Create;

public class CreateToDoItemCommand : IRequest<Result<CreateToDoItemResponse, Success, Error>>
{
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public Guid? UserId { get; set; }
}
