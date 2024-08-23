using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Jugadores.Models;
using Jugadores.Data;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Jugadores.Controllers;

public class GraficosController : Controller
{
    private ApplicationDbContext _context;

    public GraficosController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Graficos()
    {
        var jugadores = _context.Jugadores.ToList();
        
        jugadores.Add(new Jugador { JugadorID = 0, Nombre = "SELECCIONE UN JUGADOR..."});
        ViewBag.JugadorID = new SelectList(jugadores.OrderBy(j => j.JugadorID), "JugadorID", "Nombre");

        return View();
    }

    public JsonResult GraficoPartidosMes(int JugadorID, int Mes, int Anio)
    {
        List<PartidosPorDia> partidosPorDias = new List<PartidosPorDia>();

        var diasDelMes = DateTime.DaysInMonth(Anio, Mes);

        DateTime fechaMes = new DateTime();

        fechaMes = fechaMes.AddMonths(Mes - 1);

        for (int i = 1; i <= diasDelMes; i++)
        {
            var diaMesMostrar = new PartidosPorDia
            {
                Dia = i,
                Mes = fechaMes.ToString("MMM").ToUpper(),
                CantidadMinutos = 0
            };
            partidosPorDias.Add(diaMesMostrar);
        }

        var partidos = _context.Partidos.Where(p => p.JugadorID == JugadorID 
        && p.FechaPartido.Month == Mes && p.FechaPartido.Year == Anio).ToList();

        foreach (var partido in partidos.OrderBy(p => p.FechaPartido))
        {
            var partidoDiaMostrar = partidosPorDias.Where(p => p.Dia == partido.FechaPartido.Day).SingleOrDefault();
            if (partidoDiaMostrar != null)
            {
                partidoDiaMostrar.CantidadMinutos += Convert.ToInt32(partido.MinutosJugados);
            }
        }

        return Json(partidosPorDias);
    }
}
