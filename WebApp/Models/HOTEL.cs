using System.ComponentModel.DataAnnotations;

namespace Proyectofinal.Models
{
    public class HOTEL
    {
        [Key]
        [Required]
        public string? COD_HOTEL { get; set; }
        [Required, MaxLength(20)]
        public string? NOMBRE { get; set; }
        [Required, MaxLength(20)]
        public string? DIRECCION { get; set; }
        [Required, MaxLength(20)]
        public string? CIUDAD { get; set; }
        [Required, MaxLength(8)]
        public string? TELEFONO { get; set; }
        [Required]
        public int NUM_PLAZAS_DISPONIBLES { get; set; }
    }
}
