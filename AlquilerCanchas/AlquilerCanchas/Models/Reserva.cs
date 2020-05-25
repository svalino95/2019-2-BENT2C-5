using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.ComponentModel.DataAnnotations;



namespace AlquilerCanchas.Models
{
    public class Reserva
    {
        [Display(Name = "ID de la Reserva")]
        public int Id { get; set; }


        [Display(Name = "ID de la Cancha")]
        public int CanchaId { get; set; }

        [Display(Name = "Cancha")]
        public Cancha Cancha { get; set; }

        [Display(Name = "Estado de la Reserva")]
        public EstadoReserva Estado { get; set; }
        public int UsuarioId { get; set; }
        
        [Display(Name = "Usuario")]
        public virtual Usuario Usuario { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Fecha de Reserva")]
        [Required(ErrorMessage = "Campo requerido")]


        public virtual DateTime FechaReserva{ get; set; }

        [Required(ErrorMessage = "Campo requerido")]
        [Display(Name = "Valor de la cancha")]
        public virtual decimal Monto{ get; set; }

        [Display(Name = "Comentarios")]
        [MaxLength(50, ErrorMessage = "La longitud máxima es de 50 caracteres")]
        public string Comentarios { get; set; }

        [Display(Name = "ID del Turno")]
        public int TurnoId { get; set; }

        [Display(Name = "Turno")]
        public Turno Turno { get; set; }


    }
}