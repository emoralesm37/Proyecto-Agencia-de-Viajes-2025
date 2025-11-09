using System.ComponentModel.DataAnnotations;

namespace Proyectofinal.Models
{
    public class Sucursal
    {
        [Key]
        public string? CodigoSucursal { get; set; }
        [Required, MaxLength(50)]
        public string? Nombre { get; set; }

        [Required, MaxLength(50)]
        public string? Direccion { get; set; }

        [MaxLength(8)]
        public string? Telefon { get; set; }
    }
}
