using System.ComponentModel.DataAnnotations;

namespace Proyectofinal.Models
{
    public class Pais
    {
        [Key]
        public string? CodigoPais { get; set; }

        [Required,MaxLength(20)]
        public string? Nombre { get; set; }
    }
}
