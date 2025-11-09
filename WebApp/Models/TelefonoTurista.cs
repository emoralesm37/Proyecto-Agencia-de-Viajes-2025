using System.ComponentModel.DataAnnotations;

namespace Proyectofinal.Models
{
    public class TelefonoTurista
    {
        [Key]
        public string? CodigoTurista { get; set; }

        [MaxLength(8)]
        public string? Telefono { get; set; }

        public string? TipoTelefono { get; set; }
    }
}
