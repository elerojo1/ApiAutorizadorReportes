using ApiAutorizadorReportes.Data;
using ApiAutorizadorReportes.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiAutorizadorReportes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;

        public UsuariosController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] Usuario login)
        {
            var user = await _appDbContext.Usuarios
                .FirstOrDefaultAsync(u => u.NombreUsuario == login.NombreUsuario && u.Contra == login.Contra);

            if (user == null)
            {
                return Unauthorized(new { mensaje = "Usuario o contraseña incorrectos" });
            }

            return Ok(new { mensaje = "Bienvenido", usuarioId = user.Id });
        }

        [HttpGet("GetUsuarios")]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuarios()
        {
            // Esto traerá la lista completa de la tabla Usuarios
            var usuarios = await _appDbContext.Usuarios.ToListAsync();
            if (usuarios == null || !usuarios.Any())
            {
                return NotFound(new { mensaje = "No hay usuarios registrados" });
            }

            return Ok(usuarios);
        }

        [HttpGet("GetUsuarioId/{id}")]
        public async Task<ActionResult<Usuario>> GetUsuarioId(int id)
        {
            var usuario = await _appDbContext.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound(new { mensaje = "Usuario no encontrado" });
            }

            return Ok(usuario);
        }
    }
}
