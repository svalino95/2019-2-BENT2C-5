using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AlquilerCanchas.Models
{
    public class Cancha
    {

        public int Id { get; set; }

        [Required]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }

        [Display(Name = "ID Del tipo de Cancha")]
        public int TipoCanchaId { get; set; }

        [Display(Name = "Tipo de Cancha")]
        public TipoCancha TipoCancha { get; set; }
        [Required]
        [Display(Name = "Precio")]
        public double Precio { get; set; }

        public int ClubId { get; set; }

        [Display(Name = "Club")]
        public Club Club { get; set; }

        [Display(Name = "Turnos de Cancha")]
        public ICollection<TurnoCancha> TurnosCancha { get; set; }


    }
}