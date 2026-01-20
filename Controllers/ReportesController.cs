using ApiAutorizadorReportes.Data;
using ApiAutorizadorReportes.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiAutorizadorReportes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportesController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        public ReportesController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        // POST: api/Reportes
        [HttpPost("Guardar")]
        public async Task<ActionResult> PostReporte([FromForm] string titulo, [FromForm] string descripcion, [FromForm] int idUsuario, IFormFile archivoImagen)
        {
            Reporte reporte = new Reporte
            {
                Titulo = titulo,
                Descripcion = descripcion,
                IdUsuario = idUsuario,
                FechaRegistro = DateTime.UtcNow,
                Estatus = "Pendiente"
            };
            reporte.Imagen = new Imagen
            {
                Nombre = archivoImagen.FileName,
                Ruta = "wwwroot/uploads"
            };
            _appDbContext.Reportes.Add(reporte);
            await _appDbContext.SaveChangesAsync();
            return Ok(reporte);
        }

        [HttpGet("GetReporteId/{id}")]
        public async Task<ActionResult<Reporte>> GetReporteId(int id)
        {
            var reporte = await _appDbContext.Reportes
                .Include(r => r.Imagen) // Incluir la entidad relacionada Imagen
                .FirstOrDefaultAsync(r => r.Id == id);
            if (reporte == null)
            {
                return NotFound(new { mensaje = "Reporte no encontrado" });
            }
            return Ok(reporte);
        }

        [HttpGet("GetReportePorUsuario/{idUsuario}")]
        public async Task<ActionResult<IEnumerable<Reporte>>> GetReportePorUsuario(int idUsuario)
        {
            var reportes = await _appDbContext.Reportes
                .Include(r => r.Imagen) // Incluir la entidad relacionada Imagen
                .Where(r => r.IdUsuario == idUsuario)
                .ToListAsync();

            if (reportes == null || !reportes.Any())
            {
                return NotFound(new { mensaje = "No se encontraron reportes para el usuario especificado" });
            }

            return Ok(reportes);
        }

        [HttpPatch("ActualizarEstatus/{id}")]
        public async Task<IActionResult> ActualizarEstatus(int id, [FromBody] string nuevoEstatus)
        {
            var reporte = await _appDbContext.Reportes.FindAsync(id);
            if (reporte == null)
            {
                return NotFound(new { mensaje = "Reporte no encontrado" });
            }

            reporte.Estatus = nuevoEstatus;
            reporte.FechaAutorización = DateTime.UtcNow;
            await _appDbContext.SaveChangesAsync();

            return NoContent();
        }
    }
}