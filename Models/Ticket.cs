using System.ComponentModel.DataAnnotations;

namespace Aerolineas.Models;

public class Ticket
{
    [Key]
    public int Id { get; set; }
    public int NumeroTicket { get; set; }
    public int ReservaId { get; set; }
    public Reserva Reserva { get; set; }
    public int UsuarioId { get; set; }
    public Usuario Usuario { get; set; }
}