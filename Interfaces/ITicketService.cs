using Aerolineas.Config;
using Aerolineas.Models;

namespace Aerolineas.Services;

public interface ITicketService
{
    Task<Ticket?> Get(int id);
    Task<List<Ticket>> GetAll();
    Task<Result<Ticket>> Create(Ticket ticket);
    Task<Result<Ticket>> Update(Ticket ticket);
    Task<Result<bool>> Delete(int id);
}