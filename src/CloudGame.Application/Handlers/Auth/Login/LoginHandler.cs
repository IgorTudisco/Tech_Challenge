using CloudGame.Domain.Handlers;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CloudGame.Application.Handlers.Auth.Login;

public class LoginHandler : IHandler<LoginCommand, LoginResponse>
{
    public async Task<LoginResponse> HandleAsync(LoginCommand command, CancellationToken cancellationToken)
    {
        Claim[] claims = [
             new(ClaimTypes.Name, "nome do usuario"),
             new(ClaimTypes.Role, "admin"),
        ];

        var key = Encoding.ASCII.GetBytes("chavesecretachavesecretachavesecretachavesecretachavesecretachavesecreta");
        var expirationDate = DateTime.UtcNow.AddHours(1);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = expirationDate,
            SigningCredentials =
              new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature),
            Audience = "CloudGame",
            Issuer = "CloudGame",
        };

        JwtSecurityTokenHandler tokenHandler = new();
        var securityToken = tokenHandler.CreateToken(tokenDescriptor);

        return new()
        {
            Token = tokenHandler.WriteToken(securityToken),
            ExpirationDate = expirationDate,
        };
    }
}
