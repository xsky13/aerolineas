using Aerolineas.DTO.Asiento;

namespace Aerolineas.DTO.Ticket;

public class TicketDTO
{
    public int Id { get; set; }
    public int NumeroTicket { get; set; }
    public int ReservaId { get; set; }
    public ReservaSimpleDTO Reserva { get; set; }
    public int UsuarioId { get; set; }
    public UsuarioSimpleDTO Usuario { get; set; }
    public int AsientoId { get; set; }
    public AsientoSimpleDTO Asiento { get; set; }
}

public class TicketSimpleDTO
{
    public int Id { get; set; }
    public int NumeroTicket { get; set; }
    public int NumeroAsiento { get; set; }
}