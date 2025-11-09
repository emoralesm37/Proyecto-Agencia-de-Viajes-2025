using System.ComponentModel.DataAnnotations;

namespace Proyectofinal.Models
{
    public class Hospedaje
    {
        [Key]
        [Required]
        public string? CodigoHospedaje { get; set; }
        [Required]
        public string? CodigoTurista { get; set; }
        [Required]
        public string? CodigoHotel { get; set; }
        [Required]
        public DateTime? FechaLlegada { get; set; }
        [Required]
        public DateTime? FechaPartida { get; set; }
        [Required]
        public string? Regimen { get; set; }
        [Required]
        public DateTime? FechaReserva { get; set; }

        public string? Estado { get; set; }
    }
}
