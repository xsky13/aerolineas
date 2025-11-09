using Aerolineas.Config;
using Aerolineas.DTO;
using Aerolineas.DTO.Ticket;
using Aerolineas.Models;

namespace Aerolineas.Interfaces;

public interface ITicketService
{
    Task<TicketDTO> EmitirTicket(CrearTicketDTO ticketDTO);
    Task<Result<bool>> EliminarTicket(int id);
    Task<Result<bool>> ValidarTicket(ValidarTicketDTO ticketDTO);
    Task<Ticket?> GetTicket(int id);
    Task<TicketDTO?> GetTicketFull(int id);
    Task<List<TicketDTO>> GetAll();
}