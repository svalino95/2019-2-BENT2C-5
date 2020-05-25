using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AlquilerCanchas.Models
{
    public class Club
    {

        public int Id { get; set; }

        [Display(Name = "Nombre del Club")]
        public string Nombre { get; set; }

        [Display(Name = "Dirección")]

        public string Direccion { get; set; }


        [Display(Name = "Listado de canchas")]
   
        public ICollection<Cancha> Canchas { get; set; }
    }
}