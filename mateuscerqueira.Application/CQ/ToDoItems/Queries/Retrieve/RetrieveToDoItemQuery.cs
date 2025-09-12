using mateuscerqueira.Application.Common.Responses;
using MediatR;

namespace mateuscerqueira.Application.CQ.ToDoItems.Queries.Retrieve;

public class RetrieveToDoItemQuery : IRequest<Result<RetrieveToDoItemResponse, Success, Error>>
{
    public Guid Id { get; set; }
}

