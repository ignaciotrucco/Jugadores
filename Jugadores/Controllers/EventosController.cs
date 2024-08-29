using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Jugadores.Models;
using Jugadores.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Jugadores.Controllers;

public class EventosController : Controller
{
    private ApplicationDbContext _context;

    public EventosController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Eventos()
    {
        var partidos = _context.Partidos.ToList();

        partidos.Add(new Partido { PartidoID = 0, Estadio = "SELECCIONE EL ESTADIO DONDE SE JUGÓ. . . " });
        ViewBag.PartidoID = new SelectList(partidos.OrderBy(j => j.PartidoID), "PartidoID", "Estadio");

        var jugadores = _context.Jugadores.ToList();

        jugadores.Add(new Jugador { JugadorID = 0, Nombre = "[TODOS LOS JUGADORES]"});
        ViewBag.JugadorID = new SelectList(jugadores.OrderBy(j => j.JugadorID), "JugadorID", "Nombre");

        return View();
    }

    public JsonResult TraerDetallePartido(int PartidoID)
    {
        // Primero, obtienes el partido con el ID especificado, incluyendo la relación con el jugador
        var partido = _context.Partidos
                              .Include(p => p.Jugador)
                              .FirstOrDefault(p => p.PartidoID == PartidoID);

        // Luego, verificas si el partido existe
        if (partido == null)
        {
            return Json(null); // o podrías devolver un mensaje de error
        }

        // Después, haces la proyección para seleccionar solo los datos necesarios
        var partidoDto = new
        {
            Nombre = partido.Jugador.Nombre,
            FechaPartido = partido.FechaPartido
        };

        // Finalmente, devuelves los datos proyectados como JSON
        return Json(partidoDto);
    }

    public JsonResult ListadoEventos(int? EventoPartidoID, int? JugadorID)
    {
        var listadoEventos = _context.EventoPartidos.Include(l => l.Partido).Include(l => l.Partido.Jugador).ToList();

        if (EventoPartidoID != null) {
            listadoEventos = _context.EventoPartidos.Where(l => l.EventoPartidoID == EventoPartidoID).ToList();
        }
        if (JugadorID != null && JugadorID != 0) {
            listadoEventos = _context.EventoPartidos.Where(l => l.Partido.JugadorID == JugadorID).ToList();
        }

        var listadoEventosMostrar = listadoEventos.Select(p => new VistaEventos {
            EventoPartidoID = p.EventoPartidoID,
            PartidoID = p.PartidoID,
            EstadioPartido = p.Partido.Estadio,
            NombreJugador = p.Partido.Jugador.Nombre,
            PuestoJugador = p.Partido.Jugador.Puesto,
            FechaPartido = p.Partido.FechaPartido,
            Descripcion = p.Descripcion
        }).ToList();

        return Json(listadoEventosMostrar);
    }

    public JsonResult GuardarEvento(int EventoPartidoID, int PartidoID, DateTime FechaEvento, string? Descripcion)
    {
        Descripcion = Descripcion.ToUpper();
        string result = "";

        if (EventoPartidoID == 0)
        {
            var nuevoEvento = new EventoPartido 
            {
                PartidoID = PartidoID,
                FechaEvento = FechaEvento,
                Descripcion = Descripcion
            };
            _context.Add(nuevoEvento);
            _context.SaveChanges();
            result = "agg";
        }
        else 
        {
            var editarEvento = _context.EventoPartidos.Where(e => e.EventoPartidoID == EventoPartidoID).SingleOrDefault();
            if (editarEvento != null)
            {
                editarEvento.PartidoID = PartidoID;
                editarEvento.FechaEvento = FechaEvento;
                editarEvento.Descripcion = Descripcion;
                _context.SaveChanges();
                result = "edit";
            }
        }



        return Json(result);
    }

    public JsonResult EliminarEvento(int EventoPartidoID) 
    {
        var eliminarEvento = _context.EventoPartidos.Find(EventoPartidoID);
        _context.Remove(eliminarEvento);
        _context.SaveChanges();

        return Json(eliminarEvento);
    }


}
