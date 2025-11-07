using System.ComponentModel.DataAnnotations;

namespace Aerolineas.Models;

public class Aeronave
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(10)]
    public string Matricula { get; set; } = string.Empty;

    [Required]
    [StringLength(50)]
    public string Modelo { get; set; } = string.Empty;

    [Required]
    public int Capacidad { get; set; }

    [Required]
    public int AnioFabricacion { get; set; }

    [Required]
    public bool EstadoOperativo { get; set; }
}