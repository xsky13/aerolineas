namespace Aerolineas.DTO;

public class CreateAeronaveDTO
{
    public string Matricula { get; set; } = string.Empty;
    public string Modelo { get; set; } = string.Empty;
    public int Capacidad { get; set; }
    public int AnioFabricacion { get; set; }
    public bool EstadoOperativo { get; set; }
}