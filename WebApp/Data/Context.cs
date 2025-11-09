using Microsoft.EntityFrameworkCore;
using Proyectofinal.Models;

namespace Proyectofinal.Data
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options) { }

        //public DbSet<EmailTurista> EmailTurista { get; set; }
        //public DbSet<Hospedaje> Hospedajes { get; set; }
        public DbSet<HOTEL> HOTEL { get; set; }
        public DbSet<Proyectofinal.Models.TURISTA> TURISTA { get; set; } = default!;
        //public DbSet<Pais> Pais { get; set; }
        //public DbSet<ReservaVuelo> reservaVuelos { get; set; }
        //public DbSet<Sucursal> sucursals { get; set; }
        //public DbSet<TelefonoTurista> telefonoTuristas { get; set; }
        //public DbSet<Turista> turistas { get; set; }
        //public DbSet<Vuelo> vuelos { get; set; }
    }
}
