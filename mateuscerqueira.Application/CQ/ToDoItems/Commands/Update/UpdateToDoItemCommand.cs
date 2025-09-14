using mateuscerqueira.Application.Common.Responses;
using MediatR;

namespace mateuscerqueira.Application.CQ.ToDoItems.Commands.Update;

public class UpdateToDoItemCommand : IRequest<Result<UpdateToDoItemResponse, Success, Error>>
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public bool IsCompleted { get; set; }
    public Guid? UserId { get; set; }
}
