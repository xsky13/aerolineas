namespace Aerolineas.Models;

public class Reserva
{
    public int Id { get; set; }
    public int UsuarioId { get; set; }
    public int VueloId { get; set; }
    public DateTime FechaReserva { get; set; }
    public bool Confirmado { get; set; }
}