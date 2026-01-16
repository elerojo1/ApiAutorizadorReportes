using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiAutorizadorReportes.Models
{
    public class Imagen
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Define que es Identity (autoincremental)
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        [Column(TypeName = "varchar(100)")]
        public string Nombre { get; set; }

        // Aquí guardaremos la ruta local del archivo
        [Required]
        public string Ruta { get; set; }
    }
}
