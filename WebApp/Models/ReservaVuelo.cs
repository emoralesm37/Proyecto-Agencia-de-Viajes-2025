using System.ComponentModel.DataAnnotations;

namespace Proyectofinal.Models
{
    public class ReservaVuelo
    {
        [Key]
        public string? CodigoReservaVuelo { get; set; }
        [Required]
        public string? CodigoTurista { get; set; }
        public string? NumeroVuelo { get; set; }
        public string? ClaseVuelo { get; set; }
        public string? FechaReserva { get; set; }
        public string? Estado { get; set; }
    }
}
