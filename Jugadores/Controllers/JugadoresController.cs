using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Jugadores.Models;
using Jugadores.Data;

namespace Jugadores.Controllers;

public class JugadoresController : Controller
{
    private ApplicationDbContext _context;

    public JugadoresController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Jugadores()
    {
        return View();
    }

    public JsonResult ListadoJugadores(int? JugadorID)
    {
        //listado de jugadores
        var jugadores = _context.Jugadores.ToList();
        jugadores = _context.Jugadores.OrderByDescending(j => j.AnioNacimiento).ToList();

        if (JugadorID != null)
        { //listado de jugadores por id 
            jugadores = _context.Jugadores.Where(j => j.JugadorID == JugadorID).ToList();
        }

        return Json(jugadores);
    }

    public JsonResult GuardarJugador(int JugadorID, string Nombre, string Puesto, int AnioNacimiento)
    {
        string resultado = "";

        Nombre = Nombre.ToUpper();
        Puesto = Puesto.ToUpper();

        if (JugadorID == 0)
        {
            var existenDatos = _context.Jugadores.Where(e => e.Nombre == Nombre).Count();
            if (existenDatos == 0)
            {
                var nuevoJugador = new Jugador
                {
                    Nombre = Nombre,
                    Puesto = Puesto,
                    AnioNacimiento = AnioNacimiento
                };
                _context.Add(nuevoJugador);
                _context.SaveChanges();
                resultado = "Jugador añadido correctamente!";
            }
            else
            {
                resultado = "Ya existe un jugador con este nombre";
            }
        }
        else
        {
            var editarJugador = _context.Jugadores.Where(e => e.JugadorID == JugadorID).SingleOrDefault();
            if (editarJugador != null)
            {

                var existeNombreJugador = _context.Jugadores.Where(e => e.Nombre == Nombre && e.JugadorID != JugadorID).Count();
                if (existeNombreJugador == 0)
                {
                    editarJugador.Nombre = Nombre;
                    editarJugador.Puesto = Puesto;
                    editarJugador.AnioNacimiento = AnioNacimiento;
                    _context.SaveChanges();
                    resultado = "Jugador editado correctamente!";
                }
                else
                {
                    resultado = "Este jugador ya existe";
                }
            }
        }

        return Json(resultado);
    }

    public JsonResult EliminarJugador(int JugadorID)
    {
        bool eliminado = false;

        var existeJugador = _context.Partidos.Where(e => e.JugadorID == JugadorID).Count();

        if (existeJugador == 0)
        {
            var eliminarJugador = _context.Jugadores.Find(JugadorID);
            _context.Remove(eliminarJugador);
            _context.SaveChanges();
            eliminado = true;
        }

        return Json(eliminado);
    }
}
