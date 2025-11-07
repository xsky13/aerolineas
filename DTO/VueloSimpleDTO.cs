using Aerolineas.Models;

namespace Aerolineas.DTO;

public class VueloSimpleDTO
{
    public int Id { get; set; }
    public string FlightCode { get; set; }
    public int? SlotId { get; set; }
    public Slot? Slot { get; set; }
    public string Origen { get; set; }
    public string Destino { get; set; }
    public DateTime HorarioSalida { get; set; }
    public DateTime HorarioLlegada { get; set; }
}