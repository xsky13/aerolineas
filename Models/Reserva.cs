namespace Aerolineas.Models;

public class Reserva
{
    public int Id { get; set; }
    public int UsuarioId { get; set; }
    public Usuario Usuario { get; set; }
    public int VueloId { get; set; }
    public Vuelo Vuelo { get; set; }
    public int AsientoId { get; set; }
    public Asiento Asiento { get; set; }
    public DateTime FechaReserva { get; set; }
    public bool Confirmado { get; set; }
    public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}