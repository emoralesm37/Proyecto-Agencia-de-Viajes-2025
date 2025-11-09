using System.ComponentModel.DataAnnotations;

namespace Proyectofinal.Models
{
    public class TURISTA
    {
        [Key]
        public string? COD_TURISTA { get; set; }
        [Required, MaxLength(20)]
        public string? NOMBRE1 { get; set; }
        [Required, MaxLength(20)]
        public string? NOMBRE2 { get; set; }
        public string? NOMBRE3 { get; set; }

        [Required, MaxLength(20)]
        public string? APELLIDO1 { get; set; }

        [Required, MaxLength(20)]
        public string? APELLIDO2 { get; set; }

        [Required, MaxLength(20)]
        public string? DIRECCION { get; set; }
        [Required]
        public string? COD_PAIS { get; set; }
        [Required]
        public string? COD_SUCURSAL { get; set; }

        //auxiliares
        public string? PAIS { get; set; }
        public string? SUCURSAL { get; set; }
        public string? NombreCompleto { get; set; }
    }
}