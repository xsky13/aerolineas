using Aerolineas.Config;
using Aerolineas.Models;
using Microsoft.EntityFrameworkCore;

namespace Aerolineas.Services;
public class TicketService : ITicketService
{
    private readonly AeroContext dbContext;

    public TicketService(AeroContext context)
    {
        dbContext = context;
    }

    public async Task<Ticket?> Get(int id)
    {
        return await dbContext.Tickets.FindAsync(id);
    }

    public async Task<List<Ticket>> GetAll()
    {
        return await dbContext.Tickets.ToListAsync();
    }

    public async Task<Result<Ticket>> Create(Ticket ticket)
    {
        dbContext.Tickets.Add(ticket);
        await dbContext.SaveChangesAsync();
        return Result<Ticket>.Ok(ticket);
    }

    public async Task<Result<Ticket>> Update(Ticket ticket)
    {
        var existingTicket = await dbContext.Tickets.FindAsync(ticket.Id);
        if (existingTicket == null)
        {
            return Result<Ticket>.Fail("Ticket not found.");
        }

        existingTicket.ReservaId = ticket.ReservaId;
        existingTicket.NumeroAsiento = ticket.NumeroAsiento;
        existingTicket.Precio = ticket.Precio;
        

        await dbContext.SaveChangesAsync();
        return Result<Ticket>.Ok(existingTicket);
    }

    public async Task<Result<bool>> Delete(int id)
    {
        var ticket = await dbContext.Tickets.FindAsync(id);
        if (ticket == null)
        {
            return Result<bool>.Fail("Ticket not found.");
        }

        dbContext.Tickets.Remove(ticket);
        await dbContext.SaveChangesAsync();
        return Result<bool>.Ok(true);
    }
}