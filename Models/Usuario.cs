using System.ComponentModel.DataAnnotations;

namespace Aerolineas.Models;

public class Usuario
{
    [Key]
    public int Id { get; set; }
    public string Nombre { get; set; }
    public string Apellido { get; set; }
    public string DNI { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public string Rol { get; set; }
    public ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();
    public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();

}