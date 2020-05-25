using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AlquilerCanchas.Models
{
    public class Barrio
    {

        public int Id { get; set; }

        [Display(Name = "Nombre del Barrio")]
        public string Descripcion { get; set; }

        [Display(Name = "Listado de canchas")]
        public ICollection<Cancha> Canchas { get; set; }
    }
}