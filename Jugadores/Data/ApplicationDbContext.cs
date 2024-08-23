using Jugadores.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Jugadores.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Jugador> Jugadores { get; set; }
    public DbSet<Partido> Partidos { get; set; }
    public DbSet<EventoPartido> EventoPartidos {get; set;}
}
