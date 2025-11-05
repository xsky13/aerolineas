namespace Aerolineas.Models;

public class Vuelo
{
    public int Id { get; set; }
    public string Estado { get; set; }
    public Aeropuerto? Origen { get; set; }
    public Aeropuerto? Destino { get; set; }
    public DateTime HorarioSalida { get; set; }
    public DateTime HorarioLlegada { get; set; }
}