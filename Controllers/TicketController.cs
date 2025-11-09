using System.Threading.Tasks;
using Aerolineas.Config;
using Aerolineas.DTO;
using Aerolineas.DTO.Ticket;
using Aerolineas.Interfaces;
using Aerolineas.Models;
using Aerolineas.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Aerolineas.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TicketController(ITicketService ticketService) : ControllerBase
{
    [Authorize]
    [HttpPost]
    public async Task<ActionResult<TicketDTO>> Emitir([FromBody] CrearTicketDTO ticketDTO)
    {
        var result = await ticketService.EmitirTicket(ticketDTO);
        return result.ToActionResult();
    }

    [Authorize]
    [HttpGet("{id}")]
    public async Task<ActionResult<TicketDTO>> GetById(int id)
    {
        var result = await ticketService.GetTicketFull(id);
        return Ok(result);
    }

    // no necesita validacion de usuario
    [HttpPost("{id}/validar")]
    public async Task<ActionResult<bool>> Validar([FromBody] ValidarTicketDTO ticketDTO,int id)
    {
        var result = await ticketService.ValidarTicket(ticketDTO);
        return result.ToActionResult();
    }

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<TicketDTO>> Get()
    {
        var result = await ticketService.GetAll();
        return Ok(result);
    }
}