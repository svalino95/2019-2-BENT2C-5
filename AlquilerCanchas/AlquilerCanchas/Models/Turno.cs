using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AlquilerCanchas.Models
{
    public class Turno
    {

        public int Id { get; set; }

        [Required(ErrorMessage = "Campo requerido")]
        [Display(Name = "Horario")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "Campo requerido")]
        [Display(Name = "Hora de inicio")]
        public TimeSpan horaInicio { get; set; }

        [Required(ErrorMessage = "Campo requerido")]
        [Display(Name = "Hora de finalalización")]

        
        public TimeSpan horaFin { get; set; }

        [Display(Name = "Listado de Turnos de Canchas")]
        public ICollection<TurnoCancha> Canchas { get; set; }

        [Display(Name = "Listado de reservas")]
        public ICollection<Reserva> Reservas { get; set; }
    }
}