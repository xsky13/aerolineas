namespace Aerolineas.DTO;

public class ReservaDTO
{
    public int Id { get; set; }
    public int UsuarioId { get; set; }
    public UsuarioSimpleDTO Usuario { get; set; }
    public int VueloId { get; set; }
    public VueloSimpleDTO Vuelo { get; set; }
    public DateTime FechaReserva { get; set; }
    public bool Confirmado { get; set; }
}