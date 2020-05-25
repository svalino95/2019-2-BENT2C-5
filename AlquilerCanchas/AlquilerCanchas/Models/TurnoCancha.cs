using System.ComponentModel.DataAnnotations;

namespace AlquilerCanchas.Models
{
    public class TurnoCancha
    {

        public int Id { get; set; }

        public int CanchaId { get; set; }

        [Display(Name = "Cancha")]
        public Cancha Cancha { get; set; }

        public int TurnoId { get; set; }

        [Display(Name = "Turno")]
        public Turno Turno { get; set; }
    }
}