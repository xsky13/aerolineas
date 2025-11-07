using Aerolineas.Config;

namespace Aerolineas.Models;

public class Vuelo
{
    public int Id { get; set; }
    public string Estado { get; set; }
    public string Origen { get; set; }
    public int? SlotId { get; set; }
    public int? AeronaveId { get; set; } 
    public Aeronave? Aeronave { get; set; }
    public string Destino { get; set; }
    public DateTime HorarioSalida { get; set; }
    public DateTime HorarioLlegada { get; set; }
}