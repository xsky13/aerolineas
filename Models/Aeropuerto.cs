using System.ComponentModel.DataAnnotations;

namespace Aerolineas.Models;

public class Aeropuerto
{
    [Key]
    public int Id { get; set; }
    public string Nombre { get; set; }
    public int Slot { get; set; }
}