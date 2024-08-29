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

    public JsonResult ListadoInforme()
    {
        List<VistaNombreJugador> listadoInformeMostrar = new List<VistaNombreJugador>();

        var listadoInforme = _context.EventoPartidos.Include(l => l.Partido).Include(l => l.Partido.Jugador).ToList();

        foreach (var listado in listadoInforme) {
            var jugadorMostrar = listadoInformeMostrar.Where(j => j.NombreJugador == listado.Partido.Jugador.Nombre).SingleOrDefault();
            if (jugadorMostrar == null) {
                jugadorMostrar = new VistaNombreJugador {
                    NombreJugador = listado.Partido.Jugador.Nombre,
                    VistaEventos = new List<VistaEventos>()
                };
                listadoInformeMostrar.Add(jugadorMostrar);
            }
            var vistaEventos = new VistaEventos {
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

}
