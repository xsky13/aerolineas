namespace Aerolineas.DTO;

public class AeronaveDTO
{
    public int Id { get; set; }
    public string Matricula { get; set; } = string.Empty;
    public string Modelo { get; set; } = string.Empty;
    public int Capacidad { get; set; }
    public int AnioFabricacion { get; set; }
    public bool EstadoOperativo { get; set; }
    public VueloSimpleDTO? Vuelo { get; set; }
}