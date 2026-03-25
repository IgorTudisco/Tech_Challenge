using CloudGame.Application.Handlers.Auth.Login;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CloudGame.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class AuthController : Controller
{
    [AllowAnonymous]
    [HttpPost]
    [Route("Login")]
    public async Task<IActionResult> Login(
        [FromBody] LoginCommand command,
        [FromServices] LoginHandler handler,
        CancellationToken cancellationToken)
    {
        var loginResult = await handler.HandleAsync(command, cancellationToken);
        return Ok(loginResult);
    }

    [HttpPost]
    [Route("testeauth")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> Teste()
    {
        return Ok();
    }
}
