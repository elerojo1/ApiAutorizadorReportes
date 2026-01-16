using ApiAutorizadorReportes.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiAutorizadorReportes.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Imagen> Imagenes { get; set; }
        public DbSet<Reporte> Reportes { get; set; }
    }
}
