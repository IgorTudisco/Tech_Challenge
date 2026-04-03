using CloudGame.Application.Handlers.UserHandler.Create;
using CloudGame.Domain.Handlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CloudGame.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : Controller
    {
        public UsersController() { }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> CreateAsync([FromBody] CreateUserCommand command , [FromServices] IHandler<CreateUserCommand, CreateUserCommandResponse> handler)
        {
            var response = await handler.HandleAsync(command, CancellationToken.None);
            return Ok(response);
        }
    }
}
