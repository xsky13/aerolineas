using System.ComponentModel.DataAnnotations;

namespace Aerolineas.Models;

public class Slot
{
    [Key]
    public int Id { get; set; }
    public string FlightCode { get; set; }
    public string Runway { get; set; }
    public string Status { get; set; } = "pendiente";
    public int? GateId { get; set; }
    public DateTime Date { get; set; }
}