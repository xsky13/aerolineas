using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Aerolineas.Config;
using Aerolineas.DTO;
using Aerolineas.Interfaces;
using Aerolineas.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Aerolineas.Services;

public class AuthService(IOptions<AuthConfig> configuration, IUserService userService) : IAuthService
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


    public async Task<Result<string>> Login(LoginDTO loginDTO)
    {
        var usuario = await userService.GetByEmail(loginDTO.Email);
        if (usuario == null)
            return Result<string>.Fail("El usuario no existe");

        if (BCrypt.Net.BCrypt.Verify(loginDTO.Password, usuario.PasswordHash))
        {
            string token = CreateToken(usuario.Id, usuario.Rol);
            return Result<string>.Ok(token);
        }

        return Result<string>.Fail("Contrasena incorrecta");
    }

    public async Task<Result<string>> Register(RegisterDTO registerDTO)
    {
        var userResult = await userService.Create(registerDTO);
        if (!userResult.Success)
            return Result<string>.Fail(userResult.Error);

        string token = CreateToken(userResult.Value.Id, userResult.Value.Rol);
        return Result<string>.Ok(token);
    }
}