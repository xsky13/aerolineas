using Aerolineas.Config;
using Aerolineas.DTO;
using Aerolineas.Models;
using Aerolineas.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Aerolineas.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ReservaController : ControllerBase
{
    private readonly IReservaService _reservaService;

    public ReservaController(IReservaService reservaService)
    {
        _reservaService = reservaService;
    }

    [HttpGet]
    public async Task<ActionResult<List<Reserva>>> GetAll()
    {
        var reservas = await _reservaService.GetAllFull();
        return Ok(reservas);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Reserva>> Get(int id)
    {
        var reserva = await _reservaService.GetFull(id);
        if (reserva == null) return NotFound("Reserva no encontrada");
        return Ok(reserva);
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<Reserva>> Create(CrearReservaDTO reserva)
    {
        var result = await _reservaService.Create(reserva);
        if (!result.Success) return BadRequest(result.Error);
        return CreatedAtAction(nameof(Get), new { id = result.Value?.Id }, result.Value);
    }

    [Authorize]
    [HttpPut("{id}")]
    public async Task<ActionResult<Reserva>> Update(int id, Reserva reserva)
    {
        if (id != reserva.Id) return BadRequest("El ID de la ruta no coincide con el ID de la reserva");
        var result = await _reservaService.Update(reserva);
        if (!result.Success) return NotFound(result.Error);
        return Ok(result.Value);
    }

    [Authorize]
    [HttpDelete("{id}")]
    public async Task<ActionResult<bool>> Delete(int id)
    {
        var result = await _reservaService.Delete(id);
        if (!result.Success) return NotFound(result.Error);
        return Ok(result.Value);
    }

    [Authorize]
    [HttpPatch("{id}/confirmar")]
    public async Task<ActionResult<Reserva>> ConfirmarReserva(int id)
    {
        var result = await _reservaService.ConfirmarReserva(id);
        if (!result.Success) return BadRequest(result.Error);
        return Ok(result.Value);
    }

    [Authorize]
    [HttpPatch("{id}/cancelar")]
    public async Task<ActionResult<ReservaDTO>> CancelarReserva(int id)
    {
        var result = await _reservaService.CancelarReserva(id);
        return result.ToActionResult();
    }
}