using Aerolineas.Config;
using Aerolineas.DTO.Ticket;
using Aerolineas.Interfaces;
using Aerolineas.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace Aerolineas.Services;

public class TicketService(IMapper mapper, AeroContext db, IVuelosService vuelosService) : ITicketService
{
    public async Task<Result<bool>> EliminarTicket(int id)
    {
        var ticket = await GetTicket(id);
        if (ticket == null) return Result<bool>.Fail("Ticket no existe");

        db.Tickets.Remove(ticket);
        await db.SaveChangesAsync();

        return Result<bool>.Ok(true);
    }

    public async Task<TicketDTO> EmitirTicket(CrearTicketDTO ticketDTO)
    {
        Ticket ticket = new()
        {
            NumeroTicket = ticketDTO.NumeroTicket,
            ReservaId = ticketDTO.ReservaId,
            UsuarioId = ticketDTO.UsuarioId
        };

        db.Tickets.Add(ticket);
        await db.SaveChangesAsync();

        return mapper.Map<TicketDTO>(ticket);
    }

    public async Task<List<TicketDTO>> GetAll(int id)
    {
        return await db.Tickets.ProjectTo<TicketDTO>(mapper.ConfigurationProvider).ToListAsync();
    }

    public async Task<Ticket?> GetTicket(int id)
    {
        return await db.Tickets.FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<TicketDTO?> GetTicketFull(int id)
    {
        return await db.Tickets.ProjectTo<TicketDTO>(mapper.ConfigurationProvider).FirstOrDefaultAsync(t => t.Id == id);

    }

    public async Task<Result<bool>> ValidarTicket(ValidarTicketDTO ticketDTO)
    {

        var vuelo = await vuelosService.GetVueloByFlightCode(ticketDTO.FlightCode);
        if (vuelo == null) return Result<bool>.Fail("El vuelo no existe");
        if (vuelo.Estado != "confirmado") return Result<bool>.Fail("El vuelo no esta confirmado");


        var ticket = await db.Tickets.FirstOrDefaultAsync(t => t.Reserva.VueloId == vuelo.Id && t.NumeroTicket == ticketDTO.NumeroTicket);
        if (ticket == null) return Result<bool>.Fail("Ticket no existe");
        if (!ticket.Reserva.Confirmado) return Result<bool>.Fail("La reserva no esta confirmada");

        return Result<bool>.Ok(true);
    }
}