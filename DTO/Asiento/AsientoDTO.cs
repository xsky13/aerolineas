namespace Aerolineas.DTO.Asiento;

public class AsientoDTO
{
    public int Id { get; set; }
    public int NumeroAsiento { get; set; }
    public int VueloId { get; set; }
    public VueloSimpleDTO Vuelo { get; set; }
}