using Aerolineas.DTO.Ticket;

namespace Aerolineas.DTO;

public class UsuarioDTO
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public string Apellido { get; set; }
    public string DNI { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public string Rol { get; set; }
    public List<ReservaSimpleDTO>? Reservas { get; set; }
    public List<TicketSimpleDTO>? Tickets { get; set; }
}