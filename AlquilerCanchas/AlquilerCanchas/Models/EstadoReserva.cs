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


        [MaxLength(50, ErrorMessage = "La longitud máxima es de 50 caracteres")]
        [Display(Description = "Clase de css")]
        public string ClaseCss { get; set; }

        [Display(Name = "Listado de Canchas")]


        public ICollection<Reserva> Reservas { get; set; }
    }
}