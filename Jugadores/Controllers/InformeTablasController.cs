using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Jugadores.Models;
using Jugadores.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Jugadores.Controllers;

public class InformeTablasController : Controller
{
    private ApplicationDbContext _context;

    public InformeTablasController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult InformeTablas()
    {
        return View();
    }

    public IActionResult InformeTresNiveles()
    {
        return View();
    }


    public JsonResult ListadoInforme()
    {
        List<VistaNombreJugador> listadoInformeMostrar = new List<VistaNombreJugador>();

        var listadoInforme = _context.EventoPartidos.Include(l => l.Partido).Include(l => l.Partido.Jugador).ToList();

        foreach (var listado in listadoInforme)
        {
            var jugadorMostrar = listadoInformeMostrar.Where(j => j.NombreJugador == listado.Partido.Jugador.Nombre).SingleOrDefault();
            if (jugadorMostrar == null)
            {
                jugadorMostrar = new VistaNombreJugador
                {
                    NombreJugador = listado.Partido.Jugador.Nombre,
                    VistaEventos = new List<VistaEventos>()
                };
                listadoInformeMostrar.Add(jugadorMostrar);
            }
            var vistaEventos = new VistaEventos
            {
                EstadioPartido = listado.Partido.Estadio,
                PuestoJugador = listado.Partido.Jugador.Puesto,
                MinutosJugados = listado.Partido.MinutosJugados,
                FechaPartido = listado.Partido.FechaPartido,
                Descripcion = listado.Descripcion
            };
            jugadorMostrar.VistaEventos.Add(vistaEventos);
        }

        return Json(listadoInformeMostrar);
    }

    public JsonResult ListadoTresNiveles()
    {
        List<VistaJugador> vistaJugador = new List<VistaJugador>();

        var listadoEventos = _context.EventoPartidos.Include(l => l.Partido.Jugador).ToList();

        foreach (var listado in listadoEventos)
        {
            var mostrarJugador = vistaJugador.Where(m => m.JugadorID == listado.Partido.JugadorID).SingleOrDefault();
            if (mostrarJugador == null) {
                mostrarJugador = new VistaJugador {
                    JugadorID = listado.Partido.JugadorID,
                    Nombre = listado.Partido.Jugador.Nombre,
                    AnioNacimiento = listado.Partido.Jugador.AnioNacimiento,
                    VistaPartidos = new List<VistaPartidos>()
                };
                vistaJugador.Add(mostrarJugador);
            }

            var mostrarPartido = new VistaPartidos {
                PartidoID = listado.PartidoID,
                JugadorID = listado.Partido.JugadorID,
                JugadorNombre = listado.Partido.Jugador.Nombre,
                FechaPartidoString = listado.Partido.FechaPartido.ToString("dd/MM/yyyy"),
                MinutosJugados = listado.Partido.MinutosJugados,
                Estadio = listado.Partido.Estadio,
                VistaEventos = new List<VistaEventos>()
            };
            mostrarJugador.VistaPartidos.Add(mostrarPartido);

            var mostrarEvento = new VistaEventos {
                EventoPartidoID = listado.EventoPartidoID,
                PartidoID = listado.PartidoID,
                Descripcion = listado.Descripcion,
            };
            mostrarPartido.VistaEventos.Add(mostrarEvento);
        }

        return Json(vistaJugador);
    }

}
