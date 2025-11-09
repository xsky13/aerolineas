using Aerolineas.DTO.Asiento;
using Aerolineas.Models;

namespace Aerolineas.DTO;

public class VueloDTO
{
    public int Id { get; set; }
    public string FlightCode { get; set; }
    public string Estado { get; set; }
    public int? SlotId { get; set; }
    public Slot? Slot { get; set; }
    public int Precio { get; set; }
    public string Origen { get; set; }
    public string Destino { get; set; }
    public DateTime HorarioSalida { get; set; }
    public DateTime HorarioLlegada { get; set; }
    public AeronaveSimpleDTO Aeronave { get; set; }
    public List<ReservaSimpleDTO> Reservas { get; set; }
    public List<AsientoSimpleDTO> Asientos { get; set; }
}