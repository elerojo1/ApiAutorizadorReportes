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

            if (archivoImagen == null || archivoImagen.Length == 0)
            {
                return BadRequest("No se subió ningún archivo de imagen.");
            }

            var folderName = Path.Combine("wwwroot", "uploads");
            var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

            if (!Directory.Exists(pathToSave))
            {
                Directory.CreateDirectory(pathToSave);
            }

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(archivoImagen.FileName);
            var fullPath = Path.Combine(pathToSave, fileName);

            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                await archivoImagen.CopyToAsync(stream);
            }

            string urlImagen = $"/uploads/{fileName}";
            reporte.Imagen = new Imagen
            {
                Nombre = fileName,
                Ruta = urlImagen // Esto es lo que Flutter usará: "http://tu-api.com/uploads/nombre.jpg"
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
        public async Task<IActionResult> ActualizarEstatus(int id, [FromBody] ActualizarEstatusDto dto)
        {
            var reporte = await _appDbContext.Reportes.FindAsync(id);
            if (reporte == null)
            {
                return NotFound(new { mensaje = "Reporte no encontrado" });
            }

            reporte.Estatus = dto.NuevoEstatus;
            reporte.NombreCompleto = dto.NombreCompleto;
            reporte.FechaAutorización = DateTime.UtcNow;
            await _appDbContext.SaveChangesAsync();

            return NoContent();
        }
    }
}