using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Jugadores.Models;
using Jugadores.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Jugadores.Controllers;

public class PartidosController : Controller
{
    private ApplicationDbContext _context;

    public PartidosController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Partidos()
    {
        var jugadores = _context.Jugadores.ToList();
        
        jugadores.Add(new Jugador { JugadorID = 0, Nombre = "SELECCIONE UN JUGADOR..."});
        ViewBag.JugadorID = new SelectList(jugadores.OrderBy(j => j.JugadorID), "JugadorID", "Nombre");

        var JugadorIDBuscar = _context.Jugadores.ToList();

        JugadorIDBuscar.Add(new Jugador { JugadorID = 0, Nombre = "TODOS LOS JUGADORES" });
        ViewBag.JugadorIDBuscar = new SelectList(JugadorIDBuscar.OrderBy(j => j.JugadorID), "JugadorID", "Nombre");

        return View();
    }

    public JsonResult ListadoPartidos(int? PartidoID, int? JugadorIDBuscar, DateOnly FechaPartidoBuscar)
    {
        var listadoPartidos = _context.Partidos.Include(l => l.Jugador).ToList();

        if(PartidoID != null) {
            listadoPartidos = _context.Partidos.Where(l => l.PartidoID == PartidoID).ToList();
        }

        if (JugadorIDBuscar != null && JugadorIDBuscar != 0) {
            listadoPartidos = listadoPartidos.Where(l => l.JugadorID == JugadorIDBuscar).ToList();
        }

        var listadoPartidosMostrar = listadoPartidos.Select(l => new VistaPartidos 
        {
            PartidoID = l.PartidoID,
            JugadorID = l.JugadorID,
            JugadorNombre = l.Jugador.Nombre,
            FechaPartido = l.FechaPartido,
            FechaPartidoString = l.FechaPartido.ToString("dd/MM/yyyy"),
            MinutosJugados = l.MinutosJugados,
            Estadio = l.Estadio
        }).ToList();

        return Json(listadoPartidosMostrar);
    }

    public JsonResult GuardarPartidos(int PartidoID, int JugadorID, DateOnly Fecha, decimal Minutos, string Estadio)
    {
        string resultado = "";
        if(PartidoID == 0)
        {
            var nuevoPartido = new Partido 
            {
                JugadorID = JugadorID,
                FechaPartido = Fecha,
                MinutosJugados = Minutos,
                Estadio = Estadio
            };
            _context.Add(nuevoPartido);
            _context.SaveChanges();
            resultado = "cheeeck";
        }
        else
        {
            var editarPartido = _context.Partidos.Where(e => e.PartidoID == PartidoID).SingleOrDefault();
            if (editarPartido != null)
            {
                editarPartido.JugadorID = JugadorID;
                editarPartido.FechaPartido = Fecha;
                editarPartido.MinutosJugados = Minutos;
                editarPartido.Estadio = Estadio;
                _context.SaveChanges();
                resultado = "yess";
            }
        }

        return Json(resultado);
    }

    public JsonResult EliminarPartido(int PartidoID)
    {
        var eliminarPartido = _context.Partidos.Find(PartidoID);
        _context.Remove(eliminarPartido);
        _context.SaveChanges();

        return Json(eliminarPartido);
    }
}