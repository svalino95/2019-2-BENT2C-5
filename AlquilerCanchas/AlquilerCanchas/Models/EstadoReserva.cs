using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AlquilerCanchas.Models
{
    public class EstadoReserva
    {

        public int Id { get; set; }

        [Display(Name = "Descripción")]
        public string Descripcion { get; set; }

        [Display(Name = "Listado de Canchas")]
        public ICollection<Cancha> Canchas { get; set; }
    }
}