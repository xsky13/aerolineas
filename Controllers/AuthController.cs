using System.Threading.Tasks;
using Aerolineas.Config;
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
        return result.ToActionResult();
    }

    public async Task<ActionResult<string>> Register([FromBody] RegisterDTO registerDTO)
    {
        var result = await authService.Register(registerDTO);
        return result.ToActionResult();
    }
}