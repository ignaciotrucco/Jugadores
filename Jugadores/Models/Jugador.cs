using System.ComponentModel.DataAnnotations;

namespace Jugadores.Models;

public class Jugador 
{
    [Key]
    public int JugadorID { get; set; }
    public string? Nombre { get; set; }
    public int AnioNacimiento { get; set; }
    public string? Puesto { get; set; }
    // public virtual ICollection<Partido> Partidos {get; set;}
}