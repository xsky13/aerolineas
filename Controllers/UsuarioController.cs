using System.Threading.Tasks;
using Aerolineas.Config;
using Aerolineas.DTO;
using Aerolineas.Interfaces;
using Aerolineas.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Aerolineas.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsuarioController(IUserService userService) : ControllerBase
{
    [Authorize]
    [HttpGet]
    public async Task<ActionResult<List<UsuarioDTO>>> Get()
    {
        var usuarios = await userService.GetAllFull();
        return Ok(usuarios);
    }

    [Authorize]
    [HttpGet("{id}")]
    public async Task<ActionResult<UsuarioDTO>> GetById(int id)
    {
        var usuario = await userService.GetFull(id);
        if (usuario == null) return NotFound();
        return Ok(usuario);
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<Usuario>> Create([FromBody] RegisterDTO registerDTO)
    {
        var usuario = await userService.Create(registerDTO);
        return usuario.ToActionResult();
    }

    [Authorize]
    [HttpPatch("{id}")]
    public async Task<ActionResult<Usuario>> Editar([FromBody] UpdateDTO updateDTO, int id)
    {
        var usuario = await userService.Update(updateDTO, id);
        return usuario.ToActionResult();
    }

    [Authorize]
    [HttpDelete("/usuarios/{id}")]
    public async Task<ActionResult<bool>> Eliminar(int id)
    {
        var usuario = await userService.Delete(id);
        return usuario.ToActionResult();
    }
}