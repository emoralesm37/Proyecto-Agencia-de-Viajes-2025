using System.ComponentModel.DataAnnotations;

namespace Proyectofinal.Models
{
    public class Vuelo
    {
        [Key]
        public string? Num_vuelo { get; set; }
        public string? FechaHora { get; set; }

        [Required, MaxLength(100) ]
        public string? Origen { get; set; }

        [Required, MaxLength(100)]
        public string? Destino { get; set; }
        public int? PlazasTotales { get; set; }
        public int? PlazasClaseTurista { get; set; }
    }
}
