namespace Aerolineas.DTO;

public class SlotResponse
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public string Runway { get; set; }
    public string Status { get; set; }
    public int GateId { get; set; }
    public string FlightCode { get; set; }
    public DateTime ReservationExpiresAt { get; set; }
}
