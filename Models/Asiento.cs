using System.ComponentModel.DataAnnotations;

namespace Aerolineas.Models;

public class Asiento
{
    [Key]
    public int Id { get; set; }
    public int NumeroAsiento { get; set; }
    public int VueloId { get; set; }
    public Vuelo Vuelo { get; set; }
}