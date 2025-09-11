using mateuscerqueira.Application.Common.Responses;
using mateuscerqueira.Application.CQ.Users.Commands.Create;
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
}