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