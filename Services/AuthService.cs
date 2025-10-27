using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Aerolineas.Config;
using Aerolineas.DTO;
using Aerolineas.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Aerolineas.Services;

public class AuthService(IOptions<AuthConfig> configuration) : IAuthService
{
    private readonly AuthConfig _config = configuration.Value;

    private string CreateToken(int userId, string userRole)
    {
        List<Claim> claims = [
            new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
            new Claim(ClaimTypes.Role, userRole)
        ];

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.Key));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            claims: claims,
            issuer: _config.Issuer,
            audience: _config.Audience,
            notBefore: DateTime.UtcNow,
            expires: DateTime.UtcNow.AddMinutes(_config.ExpMinutes),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }


    public Task<string> Login(LoginDTO loginDTO)
    {
        // verificar que existe usuarios

        // verificar que contrasena sea correcta

        // crear token y retornar


        throw new NotImplementedException();
    }

    public Task<string> Register(RegisterDTO registerDTO)
    {
        throw new NotImplementedException();
    }
}