using Microsoft.AspNetCore.Mvc;
using System.Threading;

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
}
