using Aerolineas.Models;

namespace Aerolineas.DTO;

public class VueloDTO
{
    public string Origen { get; set; }
    public string Destino { get; set; }
    public DateTime HorarioSalida { get; set; }
    public DateTime HorarioLlegada { get; set; }
    public Aeronave? Aeronave { get; set; }
}

public class UpdateVueloDTO
{
    public string? Origen { get; set; }
    public string? Destino { get; set; }
    public DateTime? HorarioSalida { get; set; }
    public DateTime? HorarioLlegada { get; set; }
}
