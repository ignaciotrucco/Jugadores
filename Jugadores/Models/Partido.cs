using System.ComponentModel.DataAnnotations;

namespace Jugadores.Models;

public class Partido
{
    [Key]
    public int PartidoID {get; set;}
    public int JugadorID {get; set;}
    public DateOnly FechaPartido {get; set;}
    public decimal MinutosJugados {get; set;}
    public string? Estadio {get; set;}
    public virtual Jugador Jugador {get; set;}
    // public virtual ICollection<EventoPartido> EventosPartidos { get; set; }
}

public class VistaPartidos {
    public int PartidoID {get; set;}
    public int JugadorID {get; set;}
    public string? JugadorNombre {get; set;}
    public DateOnly FechaPartido {get; set;}
    public string? FechaPartidoString {get; set;}
    public decimal MinutosJugados {get; set;}
    public string? Estadio {get; set;}
}

public class PartidosPorDia 
{
    public int Dia { get; set; }
    public string? Mes { get; set; }
    public int CantidadMinutos { get; set; } 
}