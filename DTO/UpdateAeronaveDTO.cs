using Aerolineas.Models;

namespace Aerolineas.DTO;

public class UpdateAeronaveDTO
{
    public string? Modelo { get; set; }
    public int? Capacidad { get; set; }
    public bool? EstadoOperativo { get; set; }
    public Vuelo? Vuelo { get; set; }
}