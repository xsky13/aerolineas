namespace Aerolineas.DTO.Ticket;

public class TicketDTO
{
    public int Id { get; set; }
    public int NumeroTicket { get; set; }
    public int ReservaId { get; set; }
    public ReservaSimpleDTO Reserva { get; set; }
    public UsuarioSimpleDTO UsuarioId { get; set; }
    public int Usuario { get; set; }
}

public class TicketSimpleDTO
{
    public int Id { get; set; }
    public int NumeroTicket { get; set; }
}