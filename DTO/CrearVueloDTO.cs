using Aerolineas.Models;

namespace Aerolineas.DTO;

public class CrearVueloDTO
{
    public string FlightCode { get; set; }
    public string Origen { get; set; }
    public string Destino { get; set; }
    public int Precio { get; set; }
    public DateTime HorarioSalida { get; set; }
    public DateTime HorarioLlegada { get; set; }
    public Aeronave? Aeronave { get; set; }
}

public class UpdateVueloDTO
{
    public string? Origen { get; set; }
    public string? Destino { get; set; }
    public int? SlotId { get; set; }
    public Slot? Slot { get; set; }
    public int? Precio { get; set; }
    public DateTime? HorarioSalida { get; set; }
    public DateTime? HorarioLlegada { get; set; }
}
