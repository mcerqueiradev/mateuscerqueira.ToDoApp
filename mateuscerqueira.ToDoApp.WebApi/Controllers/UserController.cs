using mateuscerqueira.Application.Common.Responses;
using mateuscerqueira.Application.CQ.Users.Commands.Create;
using mateuscerqueira.Application.CQ.Users.Commands.Delete;
using mateuscerqueira.Application.CQ.Users.Commands.Update;
using mateuscerqueira.Application.CQ.Users.Queries.Retrieve;
using mateuscerqueira.Application.CQ.Users.Queries.RetrieveAll;
using mateuscerqueira.ToDoApp.Domain.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace mateuscerqueira.ToDoApp.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController(ISender sender) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(CreateUserResponse), 200)]
    [ProducesResponseType(typeof(Error), 400)]
    [ProducesResponseType(typeof(Error), 409)]
    public async Task<IActionResult> Create([FromBody] CreateUserCommand command, CancellationToken cancellationToken)
    {
        var result = await sender.Send(command, cancellationToken);

        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }

        if (result.Error is Error error && error.Code == "ExistingUser")
        {
            return Conflict(error);
        }

        return BadRequest(result.Error);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(
        [FromRoute] Guid id,
        [FromBody] UpdateUserCommand command,
        CancellationToken cancellationToken)
    {
        command.UserId = id;
        var result = await sender.Send(command, cancellationToken);

        if (result.IsSuccess)
            return Ok(result.Value);

        return result.Error.Code switch
        {
            "USER_NOT_FOUND" => NotFound(result.Error),
            "EXISTING_USER" => Conflict(result.Error),
            "INVALID_PASSWORD" => BadRequest(result.Error),
            _ => BadRequest(result.Error)
        };
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(RetrieveUserResponse), 200)]
    [ProducesResponseType(typeof(Error), 404)]
    public async Task<IActionResult> GetById([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var result = await sender.Send(new RetrieveUserQuery { Id = id }, cancellationToken);

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

    [HttpGet]
    [ProducesResponseType(typeof(PaginatedResult<RetrieveUserResponse>), 200)]
    public async Task<IActionResult> GetAll(
    CancellationToken cancellationToken,
    [FromQuery] int page = 1,
    [FromQuery] int pageSize = 20,
    [FromQuery] string? search = null,
    [FromQuery] bool? isActive = null)
    {
        var result = await sender.Send(new RetrieveAllUsersQuery
        {
            Page = page,
            PageSize = pageSize,
            SearchTerm = search,
            IsActive = isActive
        }, cancellationToken);

        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }

        return BadRequest(result.Error);
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(typeof(string), 200)]
    [ProducesResponseType(typeof(Error), 404)]
    [ProducesResponseType(typeof(Error), 400)]
    public async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var result = await sender.Send(new DeleteUserCommand { UserId = id }, cancellationToken);

        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }

        return result.Error.Code switch
        {
            "USER_NOT_FOUND" => NotFound(result.Error),
            "USER_HAS_TASKS" => BadRequest(result.Error),
            _ => BadRequest(result.Error)
        };
    }
}