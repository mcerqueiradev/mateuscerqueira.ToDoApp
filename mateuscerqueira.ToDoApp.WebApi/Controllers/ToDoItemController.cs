using mateuscerqueira.Application.Common.Responses;
using mateuscerqueira.Application.CQ.ToDoItems.Commands.Create;
using mateuscerqueira.Application.CQ.ToDoItems.Commands.Delete;
using mateuscerqueira.Application.CQ.ToDoItems.Commands.MarkAsComplete;
using mateuscerqueira.Application.CQ.ToDoItems.Commands.Update;
using mateuscerqueira.Application.CQ.ToDoItems.Queries.Retrieve;
using mateuscerqueira.Application.CQ.ToDoItems.Queries.RetrieveAll;
using mateuscerqueira.Application.CQ.ToDoItems.Queries.RetrieveAllByUserId;
using mateuscerqueira.ToDoApp.Domain.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace mateuscerqueira.ToDoApp.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ToDoItemController(ISender sender) : Controller
{
    [HttpPost]
    [ProducesResponseType(typeof(CreateToDoItemResponse), 200)]
    [ProducesResponseType(typeof(Error), 400)]
    [ProducesResponseType(typeof(Error), 404)]
    public async Task<IActionResult> Create(
    [FromBody] CreateToDoItemCommand command,
    CancellationToken cancellationToken)
    {
        var result = await sender.Send(command, cancellationToken);

        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }

        return result.Error.Code switch
        {
            "USER_NOT_FOUND" => NotFound(result.Error),
            _ => BadRequest(result.Error)
        };
    }


    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(UpdateToDoItemResponse), 200)]
    [ProducesResponseType(typeof(Error), 400)]
    [ProducesResponseType(typeof(Error), 404)]
    public async Task<IActionResult> Update(
        Guid id,
        [FromBody] UpdateToDoItemCommand command,
        CancellationToken cancellationToken)
    {
        if (id != command.Id)
        {
            return BadRequest(new Error("INVALID_ID", "Route ID does not match request body ID"));
        }

        var result = await sender.Send(command, cancellationToken);

        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }

        return result.Error.Code switch
        {
            "TODOITEM_NOT_FOUND" => NotFound(result.Error),
            "USER_NOT_FOUND" => NotFound(result.Error),
            _ => BadRequest(result.Error)
        };

    }

    [HttpPatch("{id}/complete")]
    [ProducesResponseType(typeof(MarkToDoItemAsCompleteResponse), 200)]
    [ProducesResponseType(typeof(Error), 404)]
    [ProducesResponseType(typeof(Error), 401)]
    public async Task<IActionResult> MarkAsComplete(
    [FromRoute] Guid id,
    [FromQuery] Guid? userId,
    CancellationToken cancellationToken)
    {
        var result = await sender.Send(new MarkToDoItemAsCompleteCommand
        {
            Id = id,
            UserId = userId
        }, cancellationToken);

        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }

        return result.Error.Code switch
        {
            "TODOITEM_NOT_FOUND" => NotFound(result.Error),
            "UNAUTHORIZED_ACCESS" => Unauthorized(result.Error),
            _ => BadRequest(result.Error)
        };
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(typeof(string), 200)]
    [ProducesResponseType(typeof(Error), 404)]
    [ProducesResponseType(typeof(Error), 400)]
    public async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var result = await sender.Send(new DeleteToDoItemCommand { Id= id }, cancellationToken);

        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }

        return result.Error.Code switch
        {
            "TODOITEM_NOT_FOUND" => NotFound(result.Error),
            _ => BadRequest(result.Error)
        };
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(RetrieveToDoItemResponse), 200)]
    [ProducesResponseType(typeof(Error), 404)]
    public async Task<IActionResult> GetById(
    [FromRoute] Guid id,
    CancellationToken cancellationToken)
    {
        var result = await sender.Send(new RetrieveToDoItemQuery { Id = id }, cancellationToken);

        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }

        if (result.Error.Code == "TODOITEM_NOT_FOUND")
        {
            return NotFound(result.Error);
        }

        return BadRequest(result.Error);
    }

    [HttpGet]
    [ProducesResponseType(typeof(PaginatedResult<RetrieveToDoItemResponse>), 200)]
    public async Task<IActionResult> GetAll(
    CancellationToken cancellationToken,
    [FromQuery] int page = 1,
    [FromQuery] int pageSize = 20,
    [FromQuery] string? search = null,
    [FromQuery] bool? completed = null,
    [FromQuery] Guid? userId = null)
    {
        var result = await sender.Send(new RetrieveAllToDoItemsQuery
        {
            Page = page,
            PageSize = pageSize,
            SearchTerm = search,
            IsCompleted = completed,
            UserId = userId
        }, cancellationToken);

        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }

        return BadRequest(result.Error);
    }

    [HttpGet("user/{userId:guid}")]
    [ProducesResponseType(typeof(PaginatedResult<RetrieveToDoItemResponse>), 200)]
    [ProducesResponseType(typeof(Error), 404)]
    public async Task<IActionResult> GetByUserId(
        [FromRoute] Guid userId,
        CancellationToken cancellationToken,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20,
        [FromQuery] string? search = null,
        [FromQuery] bool? completed = null)
    {
        var result = await sender.Send(new RetrieveAllToDoItemsByUserIdQuery
        {
            UserId = userId,
            Page = page,
            PageSize = pageSize,
            SearchTerm = search,
            IsCompleted = completed
        }, cancellationToken);

        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }

        if (result.Error.Code == "USER_NOT_FOUND")
        {
            return NotFound(result.Error);
        }

        return BadRequest(result.Error);
    }
}
