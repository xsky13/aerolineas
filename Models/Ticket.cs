namespace Aerolineas.Models;

public class Ticket
{
    public int Id { get; set; }
    public int ReservaId { get; set; }
    public string NumeroAsiento { get; set; }
    public decimal Precio { get; set; }
}