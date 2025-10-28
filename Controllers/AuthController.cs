using System.Threading.Tasks;
using Aerolineas.DTO;
using Aerolineas.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Aerolineas.Controllers;

[ApiController]
public class AuthController(IAuthService authService) : ControllerBase
{
    [HttpPost("/login")]
    public async Task<ActionResult<string>> Login([FromBody] LoginDTO loginDTO)
    {
        var result = await authService.Login(loginDTO);
        if (result == null) return BadRequest(new { error = "Contrasena o email invalido" });
        return Ok(result);
    }

    public async Task<ActionResult<string>> Register([FromBody] RegisterDTO registerDTO)
    {
        var result = await authService.Register(registerDTO);
        return Ok(result);
    }
}