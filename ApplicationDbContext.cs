using Microsoft.EntityFrameworkCore;
using WebAPI1990081.Entidades;

namespace WebAPI1990081
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) 
        { 
            
        }

        public DbSet<Juego> Juegos { get; set; } //juegos es el nombre de la tabla y tiene las propiedades de Juego
        public DbSet<Plataforma> Plataforma { get; set;}
    }
}
