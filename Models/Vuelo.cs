using Aerolineas.Config;

namespace Aerolineas.Models;

public class Vuelo
{
    public int Id { get; set; }
    public string FlightCode { get; set; }
    public string Estado { get; set; }
    public string Origen { get; set; }
    public int? SlotId { get; set; }
    public Slot? Slot { get; set; }
    public int? AeronaveId { get; set; } 
    public Aeronave? Aeronave { get; set; }
    public ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();
    public ICollection<Asiento> Asientos { get; set; } = new List<Asiento>();
    public string Destino { get; set; }
    public int Precio { get; set; }
    public DateTime HorarioSalida { get; set; }
    public DateTime HorarioLlegada { get; set; }
}