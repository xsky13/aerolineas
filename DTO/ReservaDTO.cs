using Aerolineas.DTO.Asiento;
using Aerolineas.DTO.Ticket;

namespace Aerolineas.DTO;

public class ReservaDTO
{
    public int Id { get; set; }
    public int UsuarioId { get; set; }
    public UsuarioSimpleDTO Usuario { get; set; }
    public int VueloId { get; set; }
    public VueloSimpleDTO Vuelo { get; set; }
    public int AsientoId { get; set; }
    public AsientoSimpleDTO Asiento { get; set; }
    public DateTime FechaReserva { get; set; }
    public bool Confirmado { get; set; }
    public List<TicketSimpleDTO>? Tickets { get; set; }

}