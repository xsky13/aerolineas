namespace Aerolineas.DTO;

public class VueloDTO
{
    public string Estado { get; set; }
    public string Origen { get; set; }
    public string Destino { get; set; }
    public DateTime HorarioSalida { get; set; }
    public DateTime HorarioLlegada { get; set; }
}
