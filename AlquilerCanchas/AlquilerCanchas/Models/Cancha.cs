using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AlquilerCanchas.Models
{
    public class Cancha
    {

        public int Id { get; set; }


        [Display(Name = "Nombre")]
        public string Nombre { get; set; }

        public int TipoCanchaId { get; set; }
        [Display(Name = "Tipo de Cancha")]
        public TipoCancha TipoCancha { get; set; }

        [Display(Name = "Dirección")]
        public string Direccion { get; set; }

        public int BarrioId { get; set; }

        [Display(Name = "Barrio")]
        public Barrio Barrio { get; set; }

        [Display(Name = "Turnos de Cancha")]
        public ICollection<TurnoCancha> TurnosCancha { get; set; }


    }
}