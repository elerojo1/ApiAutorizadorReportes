using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiAutorizadorReportes.Models
{
    public class Reporte
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Define que es Identity (autoincremental)
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Titulo { get; set; }

        [Required]
        public string Descripcion { get; set; }

        // Relación con la entidad Imagen que creamos antes
        public int IdImagen { get; set; }
        [ForeignKey("IdImagen")]
        public Imagen Imagen { get; set; }

        public DateTime FechaRegistro { get; set; } = DateTime.Now;

        [StringLength(20)]
        public string Estatus { get; set; } = "Pendiente"; // Ejemplo: "Pendiente", "Aceptado", "Rechazado"

        // Relación con el Usuario que crea el reporte
        public int IdUsuario { get; set; }
        [ForeignKey("IdUsuario")]
        public Usuario Usuario { get; set; }

        // Este campo es opcional (null) hasta que alguien lo autorice
        public DateTime? FechaAutorización
        {
            get; set;
        }
    }
}
