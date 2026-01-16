using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiAutorizadorReportes.Models
{
    public class Usuario
    {
        [Key] // Define el ID como Primary Key
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Define que es Identity (autoincremental)
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string NombreUsuario { get; set; }

        [Required]
        [StringLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string Contra { get; set; }
    }
}
