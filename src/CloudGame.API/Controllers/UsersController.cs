using CloudGame.API.Extensions;
using CloudGame.Application.Handlers.UserHandler.ChangeActive;
using CloudGame.Application.Handlers.UserHandler.Create;
using CloudGame.Application.Handlers.UserHandler.Update;
using CloudGame.Domain.Handlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CloudGame.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : Controller
{
    public UsersController() { }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> CreateAsync(
        [FromBody] CreateUserCommand command,
        [FromServices] IHandler<CreateUserCommand, CreateUserCommandResponse> handler,
        CancellationToken cancellationToken)
    {
        var result = await handler.HandleAsync(command, cancellationToken);
        return result.ToActionResult();
    }

    [HttpPut]    
    public async Task<IActionResult> UpdateAsync(
        [FromBody] UpdateUserCommand command,
        [FromServices] IHandler<UpdateUserCommand, UpdateUserResponse> handler,
        CancellationToken cancellationToken)
    {
        var result = await handler.HandleAsync(command, cancellationToken);
        return result.ToActionResult();
    }

    [HttpPost("ChangeActiveUser")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> ChangeActiveUserAsync(
        [FromBody] ChangeActiveUserCommand command,
        [FromServices] IHandler<ChangeActiveUserCommand> handler,
        CancellationToken cancellationToken)
    {
        var result = await handler.HandleAsync(command, cancellationToken);
        return result.ToActionResult();
    }
}
