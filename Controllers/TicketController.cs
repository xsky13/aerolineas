using Aerolineas.Config;
using Aerolineas.Models;
using Aerolineas.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Aerolineas.Controllers;
[ApiController]
[Route("api/[controller]")]
[Authorize]
public class TicketController : ControllerBase
{
    private readonly ITicketService _ticketService;
    public TicketController(ITicketService ticketService)
    {
        _ticketService = ticketService;
    }
    [HttpGet]
    public async Task<ActionResult<List<Ticket>>> GetAll()
    {
        var tickets = await _ticketService.GetAll();
        return Ok(tickets);
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<Ticket>> Get(int id)
    {
        var ticket = await _ticketService.Get(id);
        if (ticket == null) return NotFound("Ticket no encontrado");
        return Ok(ticket);
    }
    [HttpPost]
    public async Task<ActionResult<Ticket>> Create(Ticket ticket)
    {
        var result = await _ticketService.Create(ticket);
        if (!result.Success) return BadRequest(result.Error);
        return CreatedAtAction(nameof(Get), new { id = result.Value?.Id }, result.Value);
    }
    [HttpPut("{id}")]
    public async Task<ActionResult<Ticket>> Update(int id, Ticket ticket)
    {
        if (id != ticket.Id) return BadRequest("El ID de la ruta no coincide con el ID del ticket");
        var result = await _ticketService.Update(ticket);
        if (!result.Success) return NotFound(result.Error);
        return Ok(result.Value);
    }
    [HttpDelete("{id}")]
    public async Task<ActionResult<bool>> Delete(int id)
    {
        var result = await _ticketService.Delete(id);
        if (!result.Success) return NotFound(result.Error);
        return Ok(result.Value);
    }
}