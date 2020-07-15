
using Microsoft.EntityFrameworkCore;



namespace AlquilerCanchas.Database
{
    public class AlquilerCanchasDbContext : DbContext
    {
        public AlquilerCanchasDbContext(DbContextOptions<AlquilerCanchasDbContext> options)
            : base(options)
        {
        }

        public DbSet<AlquilerCanchas.Models.Club> Club { get; set; }

        public DbSet<AlquilerCanchas.Models.Reserva> Reserva { get; set; }

        public DbSet<AlquilerCanchas.Models.Turno> Turno { get; set; }

        public DbSet<AlquilerCanchas.Models.Usuario> Usuario { get; set; }

        public DbSet<AlquilerCanchas.Models.Cancha> Cancha { get; set; }

        public DbSet<AlquilerCanchas.Models.TipoCancha> TipoCancha { get; set; }

        public DbSet<AlquilerCanchas.Models.TurnoCancha> TurnoCancha { get; set; }

        public DbSet<AlquilerCanchas.Models.EstadoReserva> EstadoReserva { get; set; }
    }
}
