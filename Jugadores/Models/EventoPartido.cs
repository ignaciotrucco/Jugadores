using System.ComponentModel.DataAnnotations;
namespace Jugadores.Models;
public class EventoPartido
{
    [Key]
    public int EventoPartidoID { get; set; }
    public int PartidoID { get; set; }
    public DateTime FechaEvento { get; set; }
    public string? Descripcion { get; set; }
    public virtual Partido Partido { get; set; }
}

public class VistaEventos {
    public int EventoPartidoID { get; set; }
    public int PartidoID { get; set; }
    public string? NombreJugador { get; set; }
    public string? EstadioPartido {get; set;}
    public string? PuestoJugador { get; set; }
    public DateOnly FechaPartido { get; set; }
    public string? Descripcion { get; set; }
}