using CloudGame.API.Extensions;
using CloudGame.Application.Handlers.Auth.Login;
using CloudGame.Domain.Handlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CloudGame.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class AuthController : Controller
{
    private readonly ILogger<AuthController> _logger;

    public AuthController(ILogger<AuthController> logger)
    {
        _logger = logger;
    }

    [AllowAnonymous]
    [HttpPost]
    [Route("Login")]
    public async Task<IActionResult> Login(
        [FromBody] LoginCommand command,
        [FromServices] IHandler<LoginCommand, LoginResponse> handler,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Login attempt for user {User}", command.User);
        var loginResult = await handler.HandleAsync(command, cancellationToken);

        _logger.LogInformation("The user {User} has successfully logged in", command.User);
        return loginResult.ToActionResult();
    }

    [HttpPost]
    [Route("testeauth")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> Teste()
    {
        return Ok();
    }
}
