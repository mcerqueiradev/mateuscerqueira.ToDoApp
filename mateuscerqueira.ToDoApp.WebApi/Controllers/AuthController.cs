using mateuscerqueira.Application.Common.Responses;
using mateuscerqueira.Application.CQ.Auth.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace mateuscerqueira.ToDoApp.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController(ISender sender) : ControllerBase
{
    [HttpPost("login")]
    [ProducesResponseType(typeof(LoginResponse), 200)]
    [ProducesResponseType(typeof(Error), 400)]
    [ProducesResponseType(typeof(Error), 401)]
    public async Task<IActionResult> Login(
        [FromBody] LoginCommand command,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(command, cancellationToken);

        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }

        return result.Error.Code switch
        {
            "INVALID_CREDENTIALS" => Unauthorized(result.Error),
            "USER_INACTIVE" => Unauthorized(result.Error),
            _ => BadRequest(result.Error)
        };
    }
}